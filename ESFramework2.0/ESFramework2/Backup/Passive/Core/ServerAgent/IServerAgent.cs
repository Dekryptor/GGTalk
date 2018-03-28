using System;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// IServerAgent 客户端与服务器之间的所有通信都可经过IServerAgent，包括要转发的P2P消息。
	/// 它的主要目的是：
	/// (1)屏蔽客户端与服务端之间的通信协议（Tcp/Udp），ITcpServerAgent、IUdpServerAgent
	/// (2)可将异步的消息请求/回复转化为同步的方法调用。	
	/// </summary>
	public interface IServerAgent
	{
		/// <summary>
		/// 如果超时仍然没有回复，则抛出超时异常
		/// 如果dataPriority != DataPriority.CanBeDiscarded ，则checkRespond只能为false
		/// </summary>     
		NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , bool checkRespond);	

		/// <summary>
		/// CommitRequest 明确制定需要查找ServiceKey为resKey的回复
		/// </summary>	
		NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , int resKey);	
	
		void Initialize();
	}

	public enum DataPriority
	{
		High ,//紧急命令
		Common ,//如普通消息，如聊天消息
		Low ,//如文件传输
		CanBeDiscarded //如视频数据、音频数据
	}
}
