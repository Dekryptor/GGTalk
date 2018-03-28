using System;

namespace ESFramework.Server.Tcp.UserManagment
{
	/// <summary>
	/// ITcpUserManager ���ڹ������������û�����Ϣ�����������ֱ�š��û���������ʱ�䣬�����û�����ʱ��
	///					�������嵥ͨ�����񱨸�������ϱ�����д�����ݿ⣩�� 
	///					��ȫ�桢��ʵʱ���û�״̬����������
	///					
	///	��ȡ�û�״̬��Ϣ����Դ��
	///	(1)��������ServiceCommited�¼�����������SomeOneDisConnected�¼�
	///	(2)��ʱ������ĵ����¼�
	///	(3)�����������ߵ�RequestWithoutRespondArrived�¼��������ʱ������е�ĳ���û�
	///	(4)Logout����
	///	
	///	ǰ�᣺
	///	(1)��Ϣͷ�а���UserID��Ϣ
	///	zhuweisky 2005.12.06
	/// </summary>
	public interface ITcpUserManager :IUserManager
	{			
		void DisposeOneUser(string userID ,DisconnectedCause cause) ;
		void DisposeOneConnection(int connectID  ,DisconnectedCause cause) ;
		void ServiceCommited(int connectID ,string userID ,int serviceKey ,int dataCount) ;
		void ActivateUser(string userID) ;		
		int	 GetUserConnectID(string userID) ;//��������ߣ����أ�1		

		ITcpUserDisplayer TcpUserDisplayer{set ;}
		IUserTaskReporter UserTaskReporter{set ;}
		int OnLineCheckSpan{get ;set ;}//OnLineCheckSpan��λΪ���ӣ������ʹ�ö�ʱ��飬��onLineCheckSpanΪ-1

	}

	
}
