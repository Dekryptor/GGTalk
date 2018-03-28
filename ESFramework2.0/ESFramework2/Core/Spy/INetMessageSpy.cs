using System;

namespace ESFramework.Core
{
	/// <summary>
	/// INetMessageSpy ���ڼ�����е�����/�ظ���Ϣ��ͨ��ֻ�ڷ����ʹ��
	/// (1)INetMessageSpy��INetMessageHook���������ڣ�INetMessageSpy���޸���Ϣ����INetMessageHook�����޸���Ϣ�������/���ܣ�
	/// (2)INetMessageSpy��SpyReceivedMsg�������Զ����յ���ĳЩ��Ϣ
	/// zhuweisky 2006.05.17
	/// </summary>
	public interface INetMessageSpy
	{
        bool Enabled { get;set;}

        /// <summary>
        /// SpyReceivedMsg ��������յ�����Ϣ����������Ϣ������false����������Ϣ
        /// </summary>       
		bool SpyReceivedMsg(NetMessage msg) ; 

        /// <summary>
        /// SpyToBeSendedMsg ������м������͵���Ϣ����ظ���Ϣ������false����������Ϣ
        /// </summary>       
		bool SpyToBeSendedMsg(NetMessage msg) ;
	}

	/// <summary>
	/// IGatewayMessageSpy ���������ز㣬��������յ�����Ϣ��Ҫ�����ĵ�һ���������IGatewayMessageSpy��
	/// ���͵���Ϣ�ڵ����������ǰ���������һ�����Ҳ��IGatewayMessageSpy
	/// </summary>
	public interface IGatewayMessageSpy :INetMessageSpy
	{
	}

	/// <summary>
	/// IInnerMessageSpy ���յ���Ϣ���ﴦ����֮ǰ���������һ���������IInnerMessageSpy��
	/// ���������صĽ����Ϣ�����ĵ�һ�����Ҳ��IInnerMessageSpy
	/// </summary>
	public interface IInnerMessageSpy :INetMessageSpy
	{
	}

	#region Empty
	public class EmptyGatewayNetMessageSpy :IGatewayMessageSpy
	{
		#region INetMessageSpy ��Ա

		public bool Enabled
		{
			set
			{
				// TODO:  ��� EmptyGatewayNetMessageSpy.Enabled setter ʵ��
			}
            get
            {
                return true;
            }
		}

		public bool SpyReceivedMsg(NetMessage msg)
		{
			// TODO:  ��� EmptyGatewayNetMessageSpy.SpyReceivedMsg ʵ��
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
		#region INetMessageSpy ��Ա

		public bool Enabled
		{
			set
			{
				// TODO:  ��� EmptyInnerMessageSpy.Enabled setter ʵ��
			}
            get
            {
                return true;
            }
		}

		public bool SpyReceivedMsg(NetMessage msg)
		{
			// TODO:  ��� EmptyInnerMessageSpy.SpyReceivedMsg ʵ��
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
