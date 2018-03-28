using System;
using System.Net ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Udp.UserManagment
{
	/// <summary>
	/// IUdpUserManager 用于管理所有在线的UDP用户。
	/// </summary>
	public interface IUdpUserManager :IUserManager
	{				
		void RegisterUser(string userID ,IPEndPoint ipe) ;
		void UnRegisterUser(string userID) ;
		void ActivateUser(string userID) ;
		void ServiceCommitted(string userID ,NetMessage msg) ;

		IPEndPoint GetUserIpe(string userID) ;
		
		INetDisplayer NetDisplayer{set ;}
		
		event CbSimpleInt UserCountChanged ;		
		int OnLineCheckSpan{set ;}//OnLineCheckSpan单位为分钟，如果不使用定时检查，则onLineCheckSpan为-1
	}	
}
