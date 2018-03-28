using System;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// ITcp Tcpͨ������Ļ����ӿڡ�
	/// ���ߣ���ΰ sky.zhuwei@163.com 
	/// </summary>
	public interface ITcp : ITcpClientsController ,IDisposable
	{
		void Initialize() ;		
		void Start() ;
		void Stop() ; //�ͷ���������

		string IPAddress{get ;set ;}
		bool   AutoOnPublicIPAddress{set ;} //���ΪTrue����IPAddress���ò�������

		int  Port{get ;set ;}		
		int  ConnectionCount{get ;} //��ǰ���ӵ�����
		int  ReceiveBuffSize{get ;set ;}
		int  MaxMessageSize{set ;} //�����ֵ���Ϣ���ȴ���MaxMessageSize�����رն�Ӧ������

		ITcpStreamDispatcher Dispatcher{set;} 
		IContractHelper      ContractHelper{set ;}
		IBufferPool          BufferPool{set ;} 
		IEsbLogger		     EsbLogger{set ;} 

		event CbSimpleInt	SomeOneConnected ;    //���� ,ConnectID		
		event CbSimpleInt	ConnectionCountChanged ;

		event CallBackDisconnect SomeOneDisConnected ; //���� ,ConnectID
		event CallBackRespond    ServiceCommitted ;//�û�����ķ���Ļظ���Ϣ	
		event CallBackRespond    ServiceDirectCommitted ;//��ӦITcpClientsController.SendData 	
	}	

	public delegate void CallBackRespond(int connectID ,NetMessage msg) ;
	public delegate void CallBackDisconnect(int connectID ,DisconnectedCause cause) ;	

	/// <summary>
	/// ITcpController ���ڷ�������������TCP�ͻ�������
	/// </summary>
	public interface ITcpClientsController
	{		
		//������ĳ���ͻ�ͬ������Ϣ
		void SendData(int ConnectID ,NetMessage msg) ;	

		//�����ر�����
		void DisposeOneConnection(int connectID ,DisconnectedCause cause) ;
	}	
}
