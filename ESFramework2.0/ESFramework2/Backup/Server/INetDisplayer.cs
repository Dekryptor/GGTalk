using System;

namespace ESFramework.Server
{
	/// <summary>
	/// INetDisplayer ������ʾ�����û�������Ϣ��
	/// ע�⣺�ڴ˿ؼ��У����е�phoneID����ConnetID.ToString�õ��ġ�
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
