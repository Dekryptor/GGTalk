using System;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// IP2PChannelManager 用于管理所有客户《＝》客户之间的P2P通道。
	/// zhuweisky 2006.06.02
	/// </summary>
	public interface IP2PChannelManager
	{	
		bool P2PChannelUsable(string destUserID) ;
		void SendMessage(string destUserID ,NetMessage msg) ;
	}

	#region EmptyP2PChannelManager
	public class EmptyP2PChannelManager :IP2PChannelManager
	{
		#region IP2PChannelManager 成员

		public bool P2PChannelUsable(string destUserID)
		{
			// TODO:  添加 EmptyP2PChannelManager.P2PChannelUsable 实现
			return false;
		}

		public void SendMessage(string destUserID, NetMessage msg)
		{
			// TODO:  添加 EmptyP2PChannelManager.SendMessage 实现
		}

		#endregion
	}
	#endregion	
}
