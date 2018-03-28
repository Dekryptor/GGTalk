using System;
using ESBasic;

namespace ESFramework.Core
{
	/// <summary>
    /// IBasicProcesser ����������������¼���˳���check.......
	/// </summary>
    public interface IBasicProcesser : IMessageProcesser
	{ 
		event CbSimpleStr RequestWithoutRespondArrived ;
		event CbLogon     SomeOneLogon ; //ֻ���û���¼�ɹ��Ŵ���
		event CbSimpleStr SomeOneLogout ;
		event CbSimpleStr SomeOneLogonAgain ; //�����ڸ��û���¼�ظ�ǰ�������ͻ����յ�֪ͨ��Ӧ���������˶Ͽ�����
	}	

	public delegate void CbLogon(string userID ,NetMessage logonMsg) ;//��֤logonMsg
}
