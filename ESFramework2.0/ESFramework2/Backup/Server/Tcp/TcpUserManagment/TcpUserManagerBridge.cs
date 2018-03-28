using System;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Tcp.UserManagment
{
	/// <summary>
	/// TcpUserManagerBridge 用于将ITcpUserManager与相关信息源关联起来。
	/// </summary>
	public class TcpUserManagerBridge
	{
        private IBasicProcesser     basicProcesser = null;
		private ITcpUserManager     tcpUserManager     = null ;
		private ITcp                theTcp			   = null ;
		private IContractHelper     contractHelper     = null ;
		private bool                directSeviceCommitEnabled = false ;
		

		public TcpUserManagerBridge()
		{
			
		}

		public void Initialize()
		{
			this.theTcp.ServiceCommitted						 += new CallBackRespond(theTcp_ServiceCommitted);
			this.theTcp.SomeOneDisConnected						 += new CallBackDisconnect(theTcp_SomeOneDisConnected);
			this.basicProcesser.RequestWithoutRespondArrived += new CbSimpleStr(basicRequestDealer_RequestWithoutRespondArrived);
            this.basicProcesser.SomeOneLogout += new CbSimpleStr(basicRequestDealer_SomeOneLogout);
            this.basicProcesser.SomeOneLogonAgain += new CbSimpleStr(basicRequestDealer_SomeOneLogonAgain);
			this.theTcp.ServiceDirectCommitted                   += new CallBackRespond(theTcp_ServiceDirectCommitted);
		}

		#region property
		public ITcpUserManager TcpUserManager
		{
			set
			{
				this.tcpUserManager = value ;
			}
		}

		public ITcp Tcp
		{
			set
			{
				this.theTcp = value ;
			}
		}

        public IBasicProcesser BasicProcesser
		{
			set
			{
                this.basicProcesser = value;
			}
		}

		public IContractHelper ContractHelper
		{
			set
			{
				this.contractHelper = value ;
			}
		}

		public bool DirectSeviceCommitEnabled
		{
			set
			{
				this.directSeviceCommitEnabled = value ;
			}
		}
		#endregion		

		private void theTcp_SomeOneDisConnected(int ConnectID, DisconnectedCause cause)
		{
			this.tcpUserManager.DisposeOneConnection(ConnectID ,cause) ;
		}

		private void basicRequestDealer_RequestWithoutRespondArrived(string userID)
		{
			this.tcpUserManager.ActivateUser(userID) ;
		}

		private void basicRequestDealer_SomeOneLogout(string userID)
		{
			this.tcpUserManager.DisposeOneUser(userID ,DisconnectedCause.Logoff) ;
		}		

		private void theTcp_ServiceCommitted(int connectID, NetMessage msg)
		{
			this.tcpUserManager.ServiceCommited(connectID ,msg.Header.UserID ,msg.Header.ServiceKey ,msg.Header.MessageBodyLength + this.contractHelper.MessageHeaderLength) ;
		}

		private void theTcp_ServiceDirectCommitted(int connectID, NetMessage msg)
		{
			if(this.directSeviceCommitEnabled)
			{			
				if(msg.Header.UserID == NetHelper.SystemUserID)
				{
					this.tcpUserManager.ServiceCommited(connectID ,msg.Header.DestUserID ,msg.Header.ServiceKey ,msg.Header.MessageBodyLength + this.contractHelper.MessageHeaderLength) ;
				}
				else
				{
					this.tcpUserManager.ServiceCommited(connectID ,msg.Header.UserID ,msg.Header.ServiceKey ,msg.Header.MessageBodyLength + this.contractHelper.MessageHeaderLength) ;
				}
			}
		
		}

		private void basicRequestDealer_SomeOneLogonAgain(string userID)
		{
			this.tcpUserManager.DisposeOneUser(userID ,DisconnectedCause.LogonAgain) ;
		}
	}
}
