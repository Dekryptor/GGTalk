using System;

namespace ESFramework.Core
{
	/// <summary>
	/// IDispatcher ��Ϣ�����������ĵ���Ϣ������ۺ���Hook��Spy��
	/// </summary>
	public interface IMessageDispatcher
	{
		IEsbLogger		   EsbLogger{set ;}		
		INetMessageHook	   NetMessageHook{set ;}//��ΪEsbNetMessageHook
		IGatewayMessageSpy GatewayMessageSpy{set;}
		IInnerMessageSpy   InnerMessageSpy{set;}	

		IContractHelper	   ContractHelper{set ;}//��������
		INakeDispatcher    NakeDispatcher{set ;} //��������

		/// <summary>
		/// DispatchMessage ���ɲ�������Ϣ
		/// </summary>		
		NetMessage DispatchMessage(NetMessage reqMsg) ;
		
		/// <summary>
		/// BeforeSendMessage ʹ���е�SingleMessage�ڷ���֮ǰ����IMessageDispatcher��Hook����Spy�Ĵ���
		/// </summary>		
		NetMessage BeforeSendMessage(NetMessage msg) ;

		event CbNetMessage MessageReceived ;
	}	
}
