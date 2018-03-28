using System;
using ESFramework.Network.Passive ;

namespace ESFramework.Network.Extend
{
	public interface IPassiveExtend
	{
		string CurrentUserID{set;}		
	}

	/// <summary>
	/// IOutter 客户端通过IOutter接口与服务器进行通信。
	/// zhuweisky 2006.08.30
	/// </summary>
	public interface IOutter :IPassiveExtend
	{		
		IContractHelper     ContractHelper{set ;}
		IMessageTransceiver MessageTransceiver{set ;}		
	}
}
