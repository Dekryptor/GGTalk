using System;

namespace ESFramework
{
	/// <summary>
	/// IServiceKeyNameMatcher 提供功能服务的相关信息。
	/// ESFramework.Network.Extend.MessageTypeManager提供了一个实现
	/// </summary>
	public interface IServiceKeyNameMatcher
	{
		string GetServiceName(int serviceKey) ;
	}
}
