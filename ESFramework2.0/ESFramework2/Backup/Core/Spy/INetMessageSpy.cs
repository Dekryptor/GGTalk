using System;

namespace ESFramework.Core
{
	/// <summary>
	/// INetMessageSpy 用于监控所有的请求/回复消息。通常只在服务端使用
	/// (1)INetMessageSpy与INetMessageHook的区别在于，INetMessageSpy不修改消息，而INetMessageHook可能修改消息（如加密/解密）
	/// (2)INetMessageSpy的SpyReceivedMsg方法可以丢弃收到的某些消息
	/// zhuweisky 2006.05.17
	/// </summary>
	public interface INetMessageSpy
	{
        bool Enabled { get;set;}

        /// <summary>
        /// SpyReceivedMsg 监控所有收到的消息，如请求消息，返回false表明丢弃消息
        /// </summary>       
		bool SpyReceivedMsg(NetMessage msg) ; 

        /// <summary>
        /// SpyToBeSendedMsg 监控所有即将发送的消息，如回复消息，返回false表明丢弃消息
        /// </summary>       
		bool SpyToBeSendedMsg(NetMessage msg) ;
	}

	/// <summary>
	/// IGatewayMessageSpy 工作于网关层，网络组件收到的消息需要经过的第一个组件就是IGatewayMessageSpy，
	/// 发送的消息在到达网络组件前经过的最后一个组件也是IGatewayMessageSpy
	/// </summary>
	public interface IGatewayMessageSpy :INetMessageSpy
	{
	}

	/// <summary>
	/// IInnerMessageSpy 接收的消息到达处理器之前经过的最后一个组件就是IInnerMessageSpy，
	/// 处理器返回的结果消息经过的第一个组件也是IInnerMessageSpy
	/// </summary>
	public interface IInnerMessageSpy :INetMessageSpy
	{
	}

	#region Empty
	public class EmptyGatewayNetMessageSpy :IGatewayMessageSpy
	{
		#region INetMessageSpy 成员

		public bool Enabled
		{
			set
			{
				// TODO:  添加 EmptyGatewayNetMessageSpy.Enabled setter 实现
			}
            get
            {
                return true;
            }
		}

		public bool SpyReceivedMsg(NetMessage msg)
		{
			// TODO:  添加 EmptyGatewayNetMessageSpy.SpyReceivedMsg 实现
			return true;
		}

        public bool SpyToBeSendedMsg(NetMessage msg)
		{
            return true;
		}

		#endregion
	}


	public class EmptyInnerMessageSpy :IInnerMessageSpy
	{
		#region INetMessageSpy 成员

		public bool Enabled
		{
			set
			{
				// TODO:  添加 EmptyInnerMessageSpy.Enabled setter 实现
			}
            get
            {
                return true;
            }
		}

		public bool SpyReceivedMsg(NetMessage msg)
		{
			// TODO:  添加 EmptyInnerMessageSpy.SpyReceivedMsg 实现
			return true;
		}

        public bool SpyToBeSendedMsg(NetMessage msg)
		{
            return true;
		}

		#endregion
	}

	#endregion
}
