using System;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Udp.UserManagment
{
	/// <summary>
	/// UdpUserManagerBridge 用于桥接IBasicRequestDealer、EsbUdp.ServiceCommitted 与IUdpUserManager。
	/// </summary>
	public class UdpUserManagerBridge
	{
		public UdpUserManagerBridge()
		{			
		}

		public void Initialize()
		{
            this.basicProcesser.SomeOneLogon += new CbLogon(basicRequestDealer_SomeOneLogon);
            this.basicProcesser.SomeOneLogout += new CbSimpleStr(basicRequestDealer_SomeOneLogout);
            this.basicProcesser.RequestWithoutRespondArrived += new CbSimpleStr(basicRequestDealer_RequestWithoutRespondArrived);
			this.esbUdp.ServiceCommitted += new CbServiceCommitted(esbUdp_ServiceCommitted);
		}

		#region property
        #region BasicProcesser
        private IBasicProcesser basicProcesser = null;
        public IBasicProcesser BasicProcesser
		{
			set
			{
                this.basicProcesser = value;
			}
		}
		#endregion

      	#region EsbUdp
		private IEsbUdp esbUdp = null ; 
		public IEsbUdp EsbUdp
		{
			set
			{
				this.esbUdp = value ;
			}
		}
		#endregion
		
		#region UdpUserManager
		private IUdpUserManager udpUserManager = null ; 
		public IUdpUserManager UdpUserManager
		{
			set
			{
				this.udpUserManager = value ;
			}
		}
		#endregion	
		#endregion

		private void basicRequestDealer_SomeOneLogout(string userID)
		{
			this.udpUserManager.UnRegisterUser(userID) ;
		}

		private void basicRequestDealer_RequestWithoutRespondArrived(string userID)
		{
			this.udpUserManager.ActivateUser(userID) ;
		}

		private void esbUdp_ServiceCommitted(string userID, NetMessage msg)
		{
			this.udpUserManager.ServiceCommitted(userID ,msg) ;
		}		

		private void basicRequestDealer_SomeOneLogon(string userID, NetMessage logonMsg)
		{
			this.udpUserManager.RegisterUser(userID ,(System.Net.IPEndPoint)logonMsg.Tag) ;
		}
	}
}
