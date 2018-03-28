using System;
using ESFramework.Core;

namespace ESFramework.Server.Tcp.UserManagment
{
	/// <summary>
	/// ToLocalClientSender 将P2P数据转发给连接在本服务器上的另一客户。
	/// </summary>
	public class ToLocalClientSender :IToClientSender
	{
		private ITcpUserManager tcpUserManager ;
		private ITcpHookSender	tcpHookSender ;

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
		public ITcpUserManager TcpUserManager
		{
			set
			{
				this.tcpUserManager = value ;
			}
		}

		public ITcpHookSender TcpHookSender
		{
			set
			{
				this.tcpHookSender = value ; 
			}
		}

        #region HookSender
        public IHookSender HookSender
        {
            get
            {
                return this.tcpHookSender;
            }
        }
        #endregion
        #endregion

		#region IToClientSender 成员
		public int HookAndSendMessage(string userID ,NetMessage msg)
		{
			bool onLine = this.tcpUserManager.IsUserOnLine(userID) ;
			if(onLine)
			{
				int connectID = this.tcpUserManager.GetUserConnectID(userID) ;
				this.tcpHookSender.HookAndSendNetMessage(connectID ,msg) ;
				return DataSendResult.Succeed ;
			}	

			this.OverdueMessageOccured(userID ,msg) ;
		
			return DataSendResult.UserIsOffLine ;
		}
		#endregion

		
	}
}
