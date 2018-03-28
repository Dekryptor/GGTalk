using System;
using System.Net ;

namespace ESFramework.Core
{
	/// <summary>
	/// IHookSender 当服务器主动给终端发消息(比如SingleMessage)时，该消息需要经过Hooklist和GatewaySpy。
	/// 如果不知道用户对应的连接或位置，则可直接使用IToClientSender
	/// </summary>
    public interface IHookSender
    {
        IMessageDispatcher MessageDispatcher { set;}
    }	
}
