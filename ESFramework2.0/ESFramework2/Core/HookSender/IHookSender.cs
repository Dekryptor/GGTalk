using System;
using System.Net ;

namespace ESFramework.Core
{
	/// <summary>
	/// IHookSender ���������������ն˷���Ϣ(����SingleMessage)ʱ������Ϣ��Ҫ����Hooklist��GatewaySpy��
	/// �����֪���û���Ӧ�����ӻ�λ�ã����ֱ��ʹ��IToClientSender
	/// </summary>
    public interface IHookSender
    {
        IMessageDispatcher MessageDispatcher { set;}
    }	
}
