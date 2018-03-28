using System;

namespace ESFramework.Server
{
	/// <summary>
	/// INetDisplayer 用于显示在线用户请求信息。
	/// 注意：在此控件中，所有的phoneID是以ConnetID.ToString得到的。
	/// </summary>
	public interface INetDisplayer
	{
		void ClearAll() ;
		void RegisterUser(string userID) ;
		void RegisterUserEx(string phoneID ,string affix) ;
		void UngisterUser(string userID );
		void RegisterUserServicedInfo(string phoneID ,string serviceName ,int dataLen) ;
	}
}
