using System;

namespace ESFramework.Core
{
	/// <summary>
    /// IMessageProcesser 网络消息处理器基础接口
	/// 2005.07.12
	/// </summary>
    public interface IMessageProcesser
	{
        NetMessage ProcessMessage(NetMessage reqMsg);		
	}		
}
