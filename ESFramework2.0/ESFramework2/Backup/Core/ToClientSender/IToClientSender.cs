using System;

namespace ESFramework.Core
{
	/// <summary>
    /// IToClientSender 将数据（一个完整的请求－－header＋body）转发给目标用户。隐藏了目标用户的位置
	/// 目标用户可能连接在其它服务器节点上	
	/// 实现：ESFramework.Server.Udp.UserManagment.ToLocalClientSender
    ///        ESFramework.Server.Tcp.UserManagment.ToLocalClientSender
	///        ESFramework.Architecture.FourTier.ToForeignClientSender
	///        ESFramework.Network.ToClientSender
    /// 可以通过ESFramework.Core.ContainerStyleToClientSender将它们组装成Sender链
	/// </summary>
	public interface IToClientSender
	{
		int HookAndSendMessage(string userID ,NetMessage msg) ;	//返回DataSendResult的常量

		event CbHookAndSendMessage OverdueMessageOccured ; //如果目标用户不在线，将触发此事件

        IHookSender HookSender { get; }         
	}

	public delegate void CbHookAndSendMessage(string userID ,NetMessage msg) ;

	//DataSendResult 可以在应用中扩展
	public class DataSendResult
	{
		public const int Succeed		  = 1 ;
		public const int FailByOtherCause = 0 ;
		public const int UserIsOffLine    = 2 ;		
	}
}
