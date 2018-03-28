using System;
using System.Collections ;
using ESBasic;

namespace ESFramework.Server
{
	/// <summary>
	/// IUserManager 用户管理基础接口。
	/// </summary>
	public interface IUserManager
	{
		void Start() ;
		void Stop() ;

		bool IsUserOnLine(string userID) ;

		ICollection GetOnlineUserList() ;//ICollection中是在线的userID		

		event CbForUserDisconn SomeOneDisconnected ;
		event CbSimpleStr      SomeOneConnected ;//UserID
		event CbSimple         Restarted ;
	}

	public delegate void CbForUserDisconn(string userID ,DisconnectedCause cause);

	public enum DisconnectedCause
	{
		Logoff ,
		InvalidMessage ,
		MessageTokenInvalid ,
		MessageSizeOverflow ,
		NetworkError ,
		CheckerTimeOut , //check消息没有收到
		LogonAgain ,
		OtherCause
	}
}
