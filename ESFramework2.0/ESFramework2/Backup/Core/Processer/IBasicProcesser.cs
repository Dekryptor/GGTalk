using System;
using ESBasic;

namespace ESFramework.Core
{
	/// <summary>
    /// IBasicProcesser 基本请求处理器。登录、退出、check.......
	/// </summary>
    public interface IBasicProcesser : IMessageProcesser
	{ 
		event CbSimpleStr RequestWithoutRespondArrived ;
		event CbLogon     SomeOneLogon ; //只有用户登录成功才触发
		event CbSimpleStr SomeOneLogout ;
		event CbSimpleStr SomeOneLogonAgain ; //必须在给用户登录回复前触发！客户端收到通知后，应立即与服务端断开连接
	}	

	public delegate void CbLogon(string userID ,NetMessage logonMsg) ;//验证logonMsg
}
