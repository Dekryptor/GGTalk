using System;

namespace ESFramework.Core
{
	/// <summary>
    /// IMessageProcesser ������Ϣ�����������ӿ�
	/// 2005.07.12
	/// </summary>
    public interface IMessageProcesser
	{
        NetMessage ProcessMessage(NetMessage reqMsg);		
	}		
}
