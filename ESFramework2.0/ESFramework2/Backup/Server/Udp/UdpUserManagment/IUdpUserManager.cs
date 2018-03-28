using System;
using System.Net ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Udp.UserManagment
{
	/// <summary>
	/// IUdpUserManager ���ڹ����������ߵ�UDP�û���
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
		int OnLineCheckSpan{set ;}//OnLineCheckSpan��λΪ���ӣ������ʹ�ö�ʱ��飬��onLineCheckSpanΪ-1
	}	
}
