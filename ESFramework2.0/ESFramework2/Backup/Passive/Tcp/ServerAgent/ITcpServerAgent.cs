using System;
using System.Net.Sockets;
using ESFramework.Passive ;
using ESBasic;

namespace ESFramework.Passive.Tcp
{
	/// <summary>
	/// ITcpServerAgent 用于将客户端(tcp请求/响应)过程模拟成本地方法调用！
	/// ESFramework.Network.Passive.Tcp 空间的核心接口！内含了ITcpPassive组件
	/// 使用此命名空间，则消息头中的RandomNum非常重要，用于匹配Request/Response
	/// 对ServiceType.Function类型的请求支持最好!
	/// zhuweisky 2006.02.08
	/// </summary>
	public interface ITcpServerAgent :IServerAgent ,IDisposable 
	{		
		IServerAgentHelper ServerAgentHelper{set ;}
		NetworkStream     NetworkStream { set;}			

		/// <summary>
		/// 清空排队等待发送的消息
		/// </summary>		
		void ClearQueue(DataPriority queueType) ;		

		event CbStream DataDiscarded;
		event CbSimple    DataLacked;
		event CbSimple        ConnectionInterrupted ;
	}
}
