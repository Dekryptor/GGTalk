using System;
using System.Threading ;
using System.Net ;
using System.Net.Sockets ;
using ESBasic;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// IXTcpListener ���ڷ�װTCP�����߼������̡߳�
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// 2005.05.23
	/// </summary>
	public interface IEsfTcpListener :IDisposable
	{
		void Start() ; //��ʼ�����������߳�
		void Stop() ;  //��ͣ�������˳������߳�		

        event CbNetworkStream TcpConnectionEstablished; //�µ�Tcp���ӳɹ�����		
	}
   
	/// <summary>
	/// XTcpListener ��IXTcpListener��Ĭ��ʵ��
	/// </summary>
    public class EsfTcpListener : IEsfTcpListener
	{
		#region members
		private TcpListener   tcpListener = null ;	
		private volatile bool stateIsStop = true ;	
		private volatile bool toDispose   = false ;
        public event CbNetworkStream TcpConnectionEstablished;
		public ManualResetEvent manualEventDispose = new ManualResetEvent(false) ;	
		#endregion
		
		#region ctor
        public EsfTcpListener(int port)
		{
			this.tcpListener = new TcpListener(port) ;
			this.tcpListener.Start() ;
			CbSimple cb = new CbSimple(this.ListenThreadMethod) ;
			cb.BeginInvoke(null , null) ;
		}

		public EsfTcpListener(int port ,string ip)
		{
			this.tcpListener = new TcpListener( IPAddress.Parse(ip),port) ;
			this.tcpListener.Start() ;
			CbSimple cb = new CbSimple(this.ListenThreadMethod) ;
			cb.BeginInvoke(null , null) ;
		}
		#endregion

		#region IXTcpListener ��Ա
		#region Start , Stop
		public void Start()
		{
			if(! this.stateIsStop)
			{
				return ;
			}

			this.tcpListener.Start();
			
			this.stateIsStop = false ;			
		}

		public void Stop()
		{
			if(this.stateIsStop)
			{
				return ;
			}	

			this.tcpListener.Stop();					
			this.stateIsStop = true ;
		}
		#endregion

		#region ListenThreadMethod
		private void ListenThreadMethod()
		{
			while(! toDispose) 
			{		
				if(this.stateIsStop)
				{
					Thread.Sleep(100) ;
					continue ;
				}

				if(! this.tcpListener.Pending())
				{
					Thread.Sleep(100) ;
					continue ;
				}									

				TcpClient tcp_client = this.tcpListener.AcceptTcpClient() ;		
				if(this.TcpConnectionEstablished != null)
				{
					this.TcpConnectionEstablished(tcp_client.GetStream()) ;
				}
			}			

			this.manualEventDispose.Set() ;
		}	
		#endregion			

		#region Dispose
		public void Dispose()
		{
			this.Stop() ;
			this.toDispose = true ;

			this.manualEventDispose.WaitOne() ;
		}
		#endregion

		#endregion

	}	
}
