using System;

namespace ESFramework.Core
{
	/// <summary>
	/// INetMessageHook 用于对请求消息和回复消息进行截获,然后处理转换这些消息，比如加密/解密、记录、追踪等。
	/// INetMessageHook 既可以在服务端使用，也可以在客户端使用，无论在哪一端，将接收到的消息交给CaptureReceivedMsg
	/// 处理，将要发送的消息交给CaptureBeforeSendMsg处理即可。
	/// zhuweisky 2005.12.28
	/// </summary>
	public interface INetMessageHook
	{
		//转换消息
		NetMessage CaptureReceivedMsg(NetMessage msg) ;//在交给处理器之前
		NetMessage CaptureBeforeSendMsg(NetMessage msg) ;//在发送出去之前	
	
		bool Enabled{get ;set ;} //是否启用本Hook

		int[] ServiceKeyHookedList{set ;}
		int[] ServiceKeyNotHookedList{set ;}
		ServiceKeyHookedStyle ServiceKeyHookedStyle{set ;}
	}

	public enum ServiceKeyHookedStyle
	{
		Default ,//Default将Hook所有消息
		HookedList , //对应ServiceKeyHookedList
		NotHookedList //对应ServiceKeyNotHookedList
	}

	#region EmptyNetMessageHook
	public class EmptyNetMessageHook :INetMessageHook
	{
		#region INetMessageHook 成员

		public NetMessage CaptureReceivedMsg(NetMessage msg)
		{			
			return msg;
		}

		public NetMessage CaptureBeforeSendMsg(NetMessage msg)
		{			
			return msg;
		}

		public bool Enabled
		{
			get
			{
				// TODO:  添加 EmptyNetMessageHook.Enabled getter 实现
				return false;
			}
			set
			{
				// TODO:  添加 EmptyNetMessageHook.Enabled setter 实现
			}
		}

		public int[] ServiceKeyHookedList
		{
			set
			{
				// TODO:  添加 EmptyNetMessageHook.ServiceKeyHookedList setter 实现
			}
		}

		public int[] ServiceKeyNotHookedList
		{
			set
			{
				// TODO:  添加 EmptyNetMessageHook.ServiceKeyNotHookedList setter 实现
			}
		}

		public ServiceKeyHookedStyle ServiceKeyHookedStyle
		{
			set
			{
				// TODO:  添加 EmptyNetMessageHook.ServiceKeyHookedStyle setter 实现
			}
		}

		#endregion
	}


	#endregion
}
