using System;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// AsynTcp 异步Tcp组件。
	/// </summary>
	public class AsynTcp :ITcp
	{
		#region members		
		private int recieveBuffSize = 1024 ;
		private int maxMessageSize = 1000000 ;//1M
		private int port = 8000 ;
		private volatile bool stop = true ;

		private IEsfTcpListener      esfTcpListener = null ;
		private ITcpStreamDispatcher tcpStreamDispatcher = null ;
		private IContextKeyManager   contextKeyManager = new ContextKeyManager() ;		
		private IContractHelper      contractHelper ;
		private IBufferPool          bufferPool ;
		#endregion

		public AsynTcp()
		{			
		}

		#region ITcp 成员

		public void Initialize()
		{
			#region 选择IP
			if(this.autoOnPublicIPAddress)
			{
				string publicIP = NetHelper.GetLocalPublicIp() ;
				if(publicIP != null)
				{
					this.esfTcpListener = new EsfTcpListener(this.port ,publicIP) ;
				}
			}
			
			if(this.esfTcpListener == null)
			{
				if((this.iPAddress == null) || (this.iPAddress == ""))
				{
					this.esfTcpListener = new EsfTcpListener(this.port) ;
				}
				else
				{
					this.esfTcpListener = new EsfTcpListener(this.port ,this.iPAddress) ;
				}
			}
			#endregion

            this.esfTcpListener.TcpConnectionEstablished += new CbNetworkStream(esfTcpListener_TcpConnectionEstablished); 
			this.contextKeyManager.StreamCountChanged  += new CbSimpleInt(contextKeyManager_StreamCountChanged);
		}

		public void Start()
		{
			if(! this.stop)
			{
				return ;
			}

			this.stop = false ;
			this.esfTcpListener.Start() ;		
		}

		public void Stop()
		{
			if(this.stop)
			{
				return ;
			}

			this.stop = true ;

			this.esfTcpListener.Stop() ;
			//关闭所有连接		
			this.contextKeyManager.DisposeAllContextKey() ;
		}

		#region property
		#region IPAddress
		private string iPAddress = null ; 
		public string IPAddress
		{
			get
			{
				return this.iPAddress ;
			}
			set
			{
				this.iPAddress = value ;
			}
		}
		#endregion

		#region AutoOnPublicIPAddress
		private bool autoOnPublicIPAddress = false ; 
		public bool AutoOnPublicIPAddress
		{
			set
			{
				this.autoOnPublicIPAddress = value ;
			}
		}
		#endregion

		public int Port
		{
			get
			{
				return this.port ;
			}
			set
			{
				this.port = value ;
			}
		}

		public int ConnectionCount
		{
			get
			{
				return this.contextKeyManager.ConnectionCount ;
			}
		}

		public int ReceiveBuffSize
		{
			get
			{
				return this.recieveBuffSize ;
			}
			set
			{
				this.recieveBuffSize = value ;
			}
		}

		public int MaxMessageSize
		{
			set
			{
				this.maxMessageSize = value ;
			}
		}

		public ITcpStreamDispatcher Dispatcher
		{
			set
			{
				this.tcpStreamDispatcher = value ;
			}
		}

		public IContractHelper ContractHelper
		{
			set
			{
				this.contractHelper = value ;
			}
		}

		public IBufferPool BufferPool
		{
			set
			{
				this.bufferPool = value ;
			}
		}

		#region EsbLogger
		private IEsbLogger esbLogger = new EmptyEsbLogger() ; 
		public IEsbLogger EsbLogger
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
		#endregion

		#region event
		public event CbSimpleInt SomeOneConnected;

		public event CbSimpleInt ConnectionCountChanged;

        public event ESFramework.Server.Tcp.CallBackDisconnect SomeOneDisConnected;

        public event ESFramework.Server.Tcp.CallBackRespond ServiceCommitted;

        public event ESFramework.Server.Tcp.CallBackRespond ServiceDirectCommitted;
		#endregion
		#endregion

		#region ITcpClientsController 成员

		public void SendData(int ConnectID, NetMessage msg)
		{
			if(msg == null)
			{
				return ;
			}

			ISafeNetworkStream stream = this.contextKeyManager.GetNetStream(ConnectID) ;
			if(stream == null)
			{
				return ;
			}

			try
			{
				byte[] bMsg = msg.ToStream() ;
				stream.Write(bMsg ,0 ,bMsg.Length) ;

				if(this.ServiceDirectCommitted != null)
				{
					this.ServiceDirectCommitted(ConnectID ,msg) ;
				}
			}
			catch(Exception ee)
			{
				if(ee is System.IO.IOException) //正在读写流的时候，连接断开
				{
					this.DisposeOneConnection(ConnectID ,DisconnectedCause.NetworkError) ;					
				}

				throw ee ;
			}
		}
		
		public void DisposeOneConnection(int connectID ,DisconnectedCause cause)
		{			
			ISafeNetworkStream stream = this.contextKeyManager.GetNetStream(connectID) ;
			if(stream == null)
			{
				return ;
			}

			this.contextKeyManager.RemoveContextKey(connectID) ;
			stream.Close() ;

			if(this.SomeOneDisConnected != null)
			{
				this.SomeOneDisConnected(connectID ,cause) ;
			}			
		}
		#endregion	

		#region IDisposable 成员

		public void Dispose()
		{
			this.Stop() ;
			this.esfTcpListener.Dispose() ;
		}

		#endregion

		#region EventHandler
		private void esfTcpListener_TcpConnectionEstablished(System.Net.Sockets.NetworkStream stream)
		{
			ISafeNetworkStream safeStream = new SafeNetworkStream(stream) ;
	
			ContextKey key = new ContextKey(safeStream ,this.recieveBuffSize) ;		
			this.contextKeyManager.InsertContextKey(key) ;
			int connectID = key.NetStream.GetHashCode() ;
			
			if(this.SomeOneConnected != null)
			{
				this.SomeOneConnected(connectID) ;
			}	
	
			key.NetStream.NetworkStream.BeginRead(key.Buffer ,0 ,this.contractHelper.MessageHeaderLength ,new AsyncCallback(this.ServeOverLap) ,key) ;			
		}

		private void contextKeyManager_StreamCountChanged(int val)
		{			
			if(this.ConnectionCountChanged != null)
			{
				this.ConnectionCountChanged(val) ;
			}
		}
		#endregion			

		#region ServeOverLap
		private void ServeOverLap(IAsyncResult ar)
		{
			ContextKey key = (ContextKey)ar.AsyncState ;
			int streamHashCode = key.NetStream.GetHashCode() ; //是SafeNetworkStream的hashcode

			try
			{	
				#region 构造RoundedMessage
				byte[] rentBuff = null ;				
				int bytesRead   = key.NetStream.NetworkStream.EndRead(ar) ;
				if(bytesRead == 0)
				{
					this.DisposeOneConnection(streamHashCode ,DisconnectedCause.NetworkError) ;	
					return ;
				}

				int headerLen   = this.contractHelper.MessageHeaderLength ;
				int exceptCount = headerLen - bytesRead ;
				NetHelper.ReceiveData(key.NetStream.NetworkStream ,key.Buffer ,bytesRead ,exceptCount) ;

				IMessageHeader header = this.contractHelper.ParseMessageHeader(key.Buffer ,0) ;	
				if(! this.contractHelper.ValidateMessageToken(header))
				{
					this.DisposeOneConnection(streamHashCode ,DisconnectedCause.MessageTokenInvalid) ;
					return ;
				}

				RoundedMessage requestMsg = new RoundedMessage() ;
				requestMsg.ConnectID      = streamHashCode ;
				requestMsg.Header         = header ;
					
				if(! key.FirstMessageExist)
				{
					requestMsg.IsFirstMessage = true ;
					key.FirstMessageExist     = true ;
				}

				if((headerLen + header.MessageBodyLength) > this.maxMessageSize)
				{
					this.DisposeOneConnection(streamHashCode ,DisconnectedCause.MessageSizeOverflow) ;
					return ;
				}
				
				if(header.MessageBodyLength >0 )
				{
					if((header.MessageBodyLength + headerLen) <= this.recieveBuffSize)
					{
						NetHelper.ReceiveData(key.NetStream ,key.Buffer ,0 ,header.MessageBodyLength) ;
						requestMsg.Body = key.Buffer ;							
					}
					else
					{						
						rentBuff = this.bufferPool.RentBuffer(header.MessageBodyLength) ;						

						NetHelper.ReceiveData(key.NetStream ,rentBuff ,0 ,header.MessageBodyLength) ;
						requestMsg.Body = rentBuff ;							
					}
				}
				#endregion					
				
				bool closeConnection = false ;
				NetMessage resMsg = this.tcpStreamDispatcher.DealRequestData(requestMsg ,ref closeConnection) ;

				if(rentBuff != null)
				{
					this.bufferPool.GivebackBuffer(rentBuff) ;
				}

				if(closeConnection)
				{
					this.DisposeOneConnection(streamHashCode ,DisconnectedCause.OtherCause) ;
					return ;
				}

				if((resMsg != null) && (! this.stop))
				{					
					byte[] bRes = resMsg.ToStream() ;
					key.NetStream.Write(bRes ,0 ,bRes.Length) ;

					if(this.ServiceCommitted != null)
					{								
						this.ServiceCommitted(streamHashCode ,resMsg) ;
					}
				}

				if(! this.stop)
				{
					key.NetStream.NetworkStream.BeginRead(key.Buffer ,0 ,this.contractHelper.MessageHeaderLength ,new AsyncCallback(this.ServeOverLap) ,key) ;					
				}
			}
			catch(Exception ee)
			{
				if(ee is System.IO.IOException) //正在读写流的时候，连接断开
				{
					this.DisposeOneConnection(streamHashCode ,DisconnectedCause.NetworkError) ;					
				}
				else
				{
					this.esbLogger.Log(ee.GetType().ToString() ,ee.Message ,"ESFramework.Network.Tcp.AsynTcp" ,ErrorLevel.Standard) ;
				}

				ee = ee ;		
			}				
		}
		#endregion	
	}	
}
