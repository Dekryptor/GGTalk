using System;
using System.Net ;
using ESFramework.Core;

namespace ESFramework.Server.Udp.UserManagment
{
	/// <summary>
	/// ToLocalClientSender 的摘要说明。
	/// </summary>
	public class ToLocalClientSender :IToClientSender
	{
		
		#region Ctor
		public ToLocalClientSender()
		{
			this.OverdueMessageOccured += new CbHookAndSendMessage(ToLocalClientSender_OfflineMessageOccured);
		}

		private void ToLocalClientSender_OfflineMessageOccured(string userID, NetMessage msg)
		{
		}
		#endregion

		public event CbHookAndSendMessage OverdueMessageOccured ;

		#region property
		#region EsbUdpSender
		private IUdpHookSender udpHookSender = null ;
        public IUdpHookSender UdpHookSender
		{
			set
			{
				this.udpHookSender = value ;
			}
		}
		#endregion
		
		#region UdpUserManager
		private IUdpUserManager udpUserManager = null ; 
		public  IUdpUserManager UdpUserManager
		{
			set
			{
				this.udpUserManager = value ;
			}
		}
		#endregion		

        #region HookSender
        public IHookSender HookSender
        {
            get
            {
                return this.udpHookSender;
            }
        } 
        #endregion

		#endregion

		#region IToClientSender 成员

		public int HookAndSendMessage(string userID, NetMessage msg)
		{
			bool onLine = this.udpUserManager.IsUserOnLine(userID) ;
			if(onLine)
			{
				IPEndPoint ipe = this.udpUserManager.GetUserIpe(userID) ;
				this.udpHookSender.HookAndSendNetMessage(ipe ,msg) ;
				return DataSendResult.Succeed ;
			}	
		
			this.OverdueMessageOccured(userID ,msg) ;
			return DataSendResult.UserIsOffLine ;
		}

		#endregion
	}
}
