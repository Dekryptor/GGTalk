using System;

namespace ESFramework.Core
{
	/// <summary>
	/// IDispatcher 消息分派器，核心的消息组件、聚合了Hook、Spy。
	/// </summary>
	public interface IMessageDispatcher
	{
		IEsbLogger		   EsbLogger{set ;}		
		INetMessageHook	   NetMessageHook{set ;}//可为EsbNetMessageHook
		IGatewayMessageSpy GatewayMessageSpy{set;}
		IInnerMessageSpy   InnerMessageSpy{set;}	

		IContractHelper	   ContractHelper{set ;}//必须设置
		INakeDispatcher    NakeDispatcher{set ;} //必须设置

		/// <summary>
		/// DispatchMessage 分派并处理消息
		/// </summary>		
		NetMessage DispatchMessage(NetMessage reqMsg) ;
		
		/// <summary>
		/// BeforeSendMessage 使所有的SingleMessage在发送之前经过IMessageDispatcher的Hook链和Spy的处理
		/// </summary>		
		NetMessage BeforeSendMessage(NetMessage msg) ;

		event CbNetMessage MessageReceived ;
	}	
}
