using System;
using System.Collections.Generic;

namespace ESFramework.Core
{
	/// <summary>
	/// ContainerStyleToClientSender 可以包含IToClientSender List，如果使用了ToClientSender，
	/// 则只需要预定其OverdueMessageOccured事件，则可获取所有离线消息产生的通知，而不用再预定内部的Sender的OverdueMessageOccured事件
	/// </summary>
	public class ContainerStyleToClientSender :IToClientSender
	{
        private IList<IToClientSender> senderList = new List<IToClientSender>();
		public event CbHookAndSendMessage OverdueMessageOccured ;

		#region Ctor
		public ContainerStyleToClientSender()
		{			
			this.OverdueMessageOccured += new CbHookAndSendMessage(ToClientSender_OfflineMessageOccured);
		}

		private void ToClientSender_OfflineMessageOccured(string userID, NetMessage msg)
		{

		}
		#endregion		

		#region property
        public List<IToClientSender> SenderList
		{
			set
			{
				this.senderList = value ;				
			}
		}

        #region HookSender
        public IHookSender HookSender
        {
            get
            {
                return null;
            }
        }
        #endregion

		private void sender_OfflineMessageOccured(string userID, NetMessage msg)
		{
			this.OverdueMessageOccured(userID ,msg) ;
		}
		#endregion

		#region IToClientSender 成员
		public int HookAndSendMessage(string userID ,NetMessage msg)
		{
			int theResult = DataSendResult.UserIsOffLine ;			

			foreach(IToClientSender sender in this.senderList)
			{
				int res = sender.HookAndSendMessage(userID ,msg) ;
				if(res == DataSendResult.Succeed)
				{
					return DataSendResult.Succeed ;
				}	
			
				if(res != DataSendResult.UserIsOffLine)
				{
					theResult = res ;
				}
			}

			this.OverdueMessageOccured(userID ,msg) ;
			return DataSendResult.UserIsOffLine ;
		}

		#endregion

		
	}
}
