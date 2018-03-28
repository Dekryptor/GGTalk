using System;

namespace ESFramework.Server.Tcp.UserManagment
{
	/// <summary>
	/// ITcpUserManager 用于管理所有在线用户的信息，包括连接字编号、用户名、上线时间，并在用户下线时，
	///					将服务清单通过任务报告者组件上报（如写入数据库）。 
	///					最全面、最实时的用户状态发布！！！
	///					
	///	获取用户状态信息的来源：
	///	(1)网络插件的ServiceCommited事件、网络插件的SomeOneDisConnected事件
	///	(2)定时检查器的掉线事件
	///	(3)基本请求处理者的RequestWithoutRespondArrived事件＝》激活定时检查器中的某个用户
	///	(4)Logout请求
	///	
	///	前提：
	///	(1)消息头中包含UserID信息
	///	zhuweisky 2005.12.06
	/// </summary>
	public interface ITcpUserManager :IUserManager
	{			
		void DisposeOneUser(string userID ,DisconnectedCause cause) ;
		void DisposeOneConnection(int connectID  ,DisconnectedCause cause) ;
		void ServiceCommited(int connectID ,string userID ,int serviceKey ,int dataCount) ;
		void ActivateUser(string userID) ;		
		int	 GetUserConnectID(string userID) ;//如果不在线，返回－1		

		ITcpUserDisplayer TcpUserDisplayer{set ;}
		IUserTaskReporter UserTaskReporter{set ;}
		int OnLineCheckSpan{get ;set ;}//OnLineCheckSpan单位为分钟，如果不使用定时检查，则onLineCheckSpan为-1

	}

	
}
