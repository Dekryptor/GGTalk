using System;
using System.Threading ;
using System.Net.Sockets ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Passive.Tcp
{
	/// <summary>
	/// TcpPassive ITcpPassive�Ĳο�ʵ�֡�
	/// TcpPassive �����е����ݷ���ί�и�ITcpAutoSender��
	/// zhuweisky 2006.01.12
	/// </summary>
	public class TcpPassive :ITcpPassive
	{
		private NetworkStream      netStream ;
		private ITcpAutoSender     tcpAutoSender;	
		private IContractHelper    contractHelper ;		
		private ManualResetEvent   disposeDoneEvent = new ManualResetEvent(false);

		private bool gotoDispose = false;	
		
		private byte[] recieveHeaderBuff ;

		public TcpPassive()
		{
			this.tcpAutoSender = new TcpAutoSender();
		}

		#region ITcpPassive ��Ա
		#region property
		public NetworkStream NetworkStream
		{
			set
			{
				if(value != null)
				{
					this.netStream = value;
					this.tcpAutoSender.NetworkStream = this.netStream;
				}
			}
		}
		

		public IContractHelper ContractHelper
		{
			set
			{
				this.contractHelper = value ;				
			}
		}

		#region MessageDispatcher
		private IMessageDispatcher messageDispatcher = null ; 
		public IMessageDispatcher MessageDispatcher
		{
			set
			{
				this.messageDispatcher = value ;
			}
		}
		#endregion

		private IEsbLogger esbLogger = new EmptyEsbLogger() ;
		public  IEsbLogger EsbLogger
		{
			set
			{
				if(value != null)
				{
					this.esbLogger = value ;
				}
			}
		}
		#endregion

		#region event
		public event CbSimple ConnectionInterrupted ;

		public event CbStream DataDiscarded
		{
			add
			{
				this.tcpAutoSender.DataDiscarded += value;
			}
			remove
			{
				this.tcpAutoSender.DataDiscarded -= value;
			}
		}

		public event CbSimple DataLacked
		{
			add
			{
				this.tcpAutoSender.DataLacked += value;
			}
			remove
			{
				this.tcpAutoSender.DataLacked -= value;
			}
		} 
		#endregion

		#region Initialize ,HookAndSendMessage
		public void Initialize()
		{
			this.gotoDispose = false;
            this.disposeDoneEvent.Reset();
			this.recieveHeaderBuff = new byte[this.contractHelper.MessageHeaderLength] ;
			this.tcpAutoSender.ConnectionInterrupted += new CbSimple(tcpAutoSender_ConnectionInterrupted);
			this.tcpAutoSender.Initialize();

			CbSimple cb = new CbSimple(this.WorkThread) ;
			cb.BeginInvoke(null ,null) ;
		}		

		/// <summary>
		/// SendMessage �ڶ���Ϣ�������任���ڷ���
		/// </summary>		
		public void HookAndSendMessage(NetMessage msg, DataPriority dataPriority)
		{
			if(msg == null)
			{
				return ;
			}

			this.tcpAutoSender.SendData(this.messageDispatcher.BeforeSendMessage(msg).ToStream(), dataPriority);	
		}		
		#endregion

		#region WorkThread
		private void WorkThread()
		{
			while(true)
			{
				if (this.gotoDispose)
				{
					break;
				}

				#region Work
				try
				{
					if(! this.netStream.DataAvailable)
					{
						Thread.Sleep(20) ;
						continue ;
					}	
			
					//��ȡ��Ϣͷ
					NetHelper.ReceiveData(this.netStream, this.recieveHeaderBuff, 0, this.recieveHeaderBuff.Length);
					IMessageHeader header = this.contractHelper.ParseMessageHeader(this.recieveHeaderBuff, 0);

					//��ȡ��Ϣ����
					byte[] body = null;
					if (header.MessageBodyLength > 0)
					{
						body = NetHelper.ReceiveData(this.netStream, header.MessageBodyLength);
					}

					NetMessage netMsg = new NetMessage(header ,body) ;
					NetMessage respond = this.messageDispatcher.DispatchMessage(netMsg) ;
					
					if (respond != null)
					{
						NetMessage hookedRespond = this.messageDispatcher.BeforeSendMessage(respond);
						this.tcpAutoSender.SendData(hookedRespond.ToStream(), DataPriority.Common);
					}					
				}
				catch(Exception ee)
				{
					this.esbLogger.Log(ee.GetType().ToString() ,ee.Message ,"ESFramework.Network.Tcp.Passive.TcpPassive" ,ErrorLevel.High) ;

					#region ConnectionInterrupted
					if(ee is System.IO.IOException) //���ڶ�д����ʱ�����ӶϿ�
					{
						this.disposeDoneEvent.Set() ; //����������ConnectionInterrupted�¼�֮ǰ
						if(this.ConnectionInterrupted != null)
						{							
							this.ConnectionInterrupted() ;							
						}
						break ;
					}
					#endregion
				}
				#endregion
			}

			this.disposeDoneEvent.Set() ;
		}
		
		#endregion

		#region ClearQueue
		public void ClearQueue(DataPriority queueType)
		{
			this.tcpAutoSender.ClearQueue(queueType) ;
		}
		#endregion

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			this.gotoDispose = true;
			this.disposeDoneEvent.WaitOne(1000, true);
			
			this.tcpAutoSender.ConnectionInterrupted -= new CbSimple(tcpAutoSender_ConnectionInterrupted);
			this.tcpAutoSender.Dispose();

			if(this.netStream != null)
			{
				this.netStream.Close() ;
				this.netStream = null ;
			}
		}

		#endregion

		private void tcpAutoSender_ConnectionInterrupted()
		{
			if(this.ConnectionInterrupted != null)
			{
				this.ConnectionInterrupted() ;
			}
		}
	}

	
}
