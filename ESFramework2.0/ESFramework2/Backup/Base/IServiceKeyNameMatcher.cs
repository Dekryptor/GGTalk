using System;

namespace ESFramework
{
	/// <summary>
	/// IServiceKeyNameMatcher �ṩ���ܷ���������Ϣ��
	/// ESFramework.Network.Extend.MessageTypeManager�ṩ��һ��ʵ��
	/// </summary>
	public interface IServiceKeyNameMatcher
	{
		string GetServiceName(int serviceKey) ;
	}
}
