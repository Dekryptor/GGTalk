using System;

namespace ESFramework.Server.Tcp.UserManagment
{
	//// <summary>
	///ITcpDisplayer 用于显示所有客户端用户的请求信息。	
	///作者：朱伟 sky.zhuwei@163.com 
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
		#region ITcpDisplayer 成员

		public void ClearAll()
		{
			// TODO:  添加 EmptyTcpDisplayer.ClearAll 实现
		}

		public void SetOrUpdateUserItem(string userID, int justServiceKey, int totalDataLen)
		{
			// TODO:  添加 EmptyTcpDisplayer.SetOrUpdateUserItem 实现
		}

		public void RemoveUser(string userID, DisconnectedCause cause)
		{
			// TODO:  添加 EmptyTcpDisplayer.RemoveUser 实现
		}
		#endregion

	}
	#endregion

}
