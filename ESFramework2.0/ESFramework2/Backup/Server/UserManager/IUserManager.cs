using System;
using System.Collections ;
using ESBasic;

namespace ESFramework.Server
{
	/// <summary>
	/// IUserManager �û���������ӿڡ�
	/// </summary>
	public interface IUserManager
	{
		void Start() ;
		void Stop() ;

		bool IsUserOnLine(string userID) ;

		ICollection GetOnlineUserList() ;//ICollection�������ߵ�userID		

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
		CheckerTimeOut , //check��Ϣû���յ�
		LogonAgain ,
		OtherCause
	}
}
