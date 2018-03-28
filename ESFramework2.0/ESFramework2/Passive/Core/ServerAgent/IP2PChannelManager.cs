using System;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// IP2PChannelManager ���ڹ������пͻ��������ͻ�֮���P2Pͨ����
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
		#region IP2PChannelManager ��Ա

		public bool P2PChannelUsable(string destUserID)
		{
			// TODO:  ��� EmptyP2PChannelManager.P2PChannelUsable ʵ��
			return false;
		}

		public void SendMessage(string destUserID, NetMessage msg)
		{
			// TODO:  ��� EmptyP2PChannelManager.SendMessage ʵ��
		}

		#endregion
	}
	#endregion	
}
