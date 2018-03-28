using System;

namespace ESFramework.Server.Tcp.UserManagment
{
	//// <summary>
	///ITcpDisplayer ������ʾ���пͻ����û���������Ϣ��	
	///���ߣ���ΰ sky.zhuwei@163.com 
	/// 2004.04.22
	/// </summary>
	public interface ITcpUserDisplayer 
	{	
		void ClearAll() ;			
		void SetOrUpdateUserItem(string userID ,int justServiceKey ,int totalDataLen) ;  		
		void RemoveUser(string userID ,DisconnectedCause cause);
	}  

	#region EmptyTcpDisplayer
	public class EmptyTcpDisplayer :ITcpUserDisplayer
	{
		#region ITcpDisplayer ��Ա

		public void ClearAll()
		{
			// TODO:  ��� EmptyTcpDisplayer.ClearAll ʵ��
		}

		public void SetOrUpdateUserItem(string userID, int justServiceKey, int totalDataLen)
		{
			// TODO:  ��� EmptyTcpDisplayer.SetOrUpdateUserItem ʵ��
		}

		public void RemoveUser(string userID, DisconnectedCause cause)
		{
			// TODO:  ��� EmptyTcpDisplayer.RemoveUser ʵ��
		}
		#endregion

	}
	#endregion

}
