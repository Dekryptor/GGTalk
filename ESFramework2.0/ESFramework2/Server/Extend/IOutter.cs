using System;
using ESFramework.Network.Passive ;

namespace ESFramework.Network.Extend
{
	public interface IPassiveExtend
	{
		string CurrentUserID{set;}		
	}

	/// <summary>
	/// IOutter �ͻ���ͨ��IOutter�ӿ������������ͨ�š�
	/// zhuweisky 2006.08.30
	/// </summary>
	public interface IOutter :IPassiveExtend
	{		
		IContractHelper     ContractHelper{set ;}
		IMessageTransceiver MessageTransceiver{set ;}		
	}
}
