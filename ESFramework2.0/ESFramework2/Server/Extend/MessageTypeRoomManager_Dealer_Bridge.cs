using System;
using ESFramework.Network.Passive ;

namespace ESFramework.Network.Extend
{
	/// <summary>
    /// MessageTypeRoomManager_Dealer_Bridge «≈Ω”MessageTypeManager ”ÎSimplePassiveDataDealer°£
	/// </summary>
	public class MessageTypeRoomManager_Dealer_Bridge
	{
        public MessageTypeRoomManager_Dealer_Bridge()
		{			
		}

		#region MessageTypeManager
        private MessageTypeRoomManager messageTypeRoomManager = null;
        public MessageTypeRoomManager MessageTypeRoomManager
		{
			set
			{
                this.messageTypeRoomManager = value;
			}
		}
		#endregion
		
		#region SimplePassiveDataDealer
		private SimplePassiveDataDealer simplePassiveDataDealer = null ; 
		public SimplePassiveDataDealer SimplePassiveDataDealer
		{
			set
			{
				this.simplePassiveDataDealer = value ;
			}
		}
		#endregion

		#region Initialize
		public void Initialize()
		{
            foreach (int pushKey in this.messageTypeRoomManager.PushKeyList)
			{
				this.simplePassiveDataDealer.AddPushKey(pushKey) ;
			}
		}
		#endregion
	}
}
