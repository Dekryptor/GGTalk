using System;

namespace ESFramework.Core
{
	/// <summary>
	/// INetMessageHook ���ڶ�������Ϣ�ͻظ���Ϣ���нػ�,Ȼ����ת����Щ��Ϣ���������/���ܡ���¼��׷�ٵȡ�
	/// INetMessageHook �ȿ����ڷ����ʹ�ã�Ҳ�����ڿͻ���ʹ�ã���������һ�ˣ������յ�����Ϣ����CaptureReceivedMsg
	/// ������Ҫ���͵���Ϣ����CaptureBeforeSendMsg�����ɡ�
	/// zhuweisky 2005.12.28
	/// </summary>
	public interface INetMessageHook
	{
		//ת����Ϣ
		NetMessage CaptureReceivedMsg(NetMessage msg) ;//�ڽ���������֮ǰ
		NetMessage CaptureBeforeSendMsg(NetMessage msg) ;//�ڷ��ͳ�ȥ֮ǰ	
	
		bool Enabled{get ;set ;} //�Ƿ����ñ�Hook

		int[] ServiceKeyHookedList{set ;}
		int[] ServiceKeyNotHookedList{set ;}
		ServiceKeyHookedStyle ServiceKeyHookedStyle{set ;}
	}

	public enum ServiceKeyHookedStyle
	{
		Default ,//Default��Hook������Ϣ
		HookedList , //��ӦServiceKeyHookedList
		NotHookedList //��ӦServiceKeyNotHookedList
	}

	#region EmptyNetMessageHook
	public class EmptyNetMessageHook :INetMessageHook
	{
		#region INetMessageHook ��Ա

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
				// TODO:  ��� EmptyNetMessageHook.Enabled getter ʵ��
				return false;
			}
			set
			{
				// TODO:  ��� EmptyNetMessageHook.Enabled setter ʵ��
			}
		}

		public int[] ServiceKeyHookedList
		{
			set
			{
				// TODO:  ��� EmptyNetMessageHook.ServiceKeyHookedList setter ʵ��
			}
		}

		public int[] ServiceKeyNotHookedList
		{
			set
			{
				// TODO:  ��� EmptyNetMessageHook.ServiceKeyNotHookedList setter ʵ��
			}
		}

		public ServiceKeyHookedStyle ServiceKeyHookedStyle
		{
			set
			{
				// TODO:  ��� EmptyNetMessageHook.ServiceKeyHookedStyle setter ʵ��
			}
		}

		#endregion
	}


	#endregion
}
