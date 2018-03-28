using System;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// ITcp Tcp通信组件的基础接口。
	/// 作者：朱伟 sky.zhuwei@163.com 
	/// </summary>
	public interface ITcp : ITcpClientsController ,IDisposable
	{
		void Initialize() ;		
		void Start() ;
		void Stop() ; //释放所有连接

		string IPAddress{get ;set ;}
		bool   AutoOnPublicIPAddress{set ;} //如果为True，则IPAddress设置不起作用

		int  Port{get ;set ;}		
		int  ConnectionCount{get ;} //当前连接的数量
		int  ReceiveBuffSize{get ;set ;}
		int  MaxMessageSize{set ;} //当发现的消息长度大于MaxMessageSize，将关闭对应的连接

		ITcpStreamDispatcher Dispatcher{set;} 
		IContractHelper      ContractHelper{set ;}
		IBufferPool          BufferPool{set ;} 
		IEsbLogger		     EsbLogger{set ;} 

		event CbSimpleInt	SomeOneConnected ;    //上线 ,ConnectID		
		event CbSimpleInt	ConnectionCountChanged ;

		event CallBackDisconnect SomeOneDisConnected ; //掉线 ,ConnectID
		event CallBackRespond    ServiceCommitted ;//用户请求的服务的回复信息	
		event CallBackRespond    ServiceDirectCommitted ;//对应ITcpClientsController.SendData 	
	}	

	public delegate void CallBackRespond(int connectID ,NetMessage msg) ;
	public delegate void CallBackDisconnect(int connectID ,DisconnectedCause cause) ;	

	/// <summary>
	/// ITcpController 用于服务器主动控制TCP客户的连接
	/// </summary>
	public interface ITcpClientsController
	{		
		//主动给某个客户同步发信息
		void SendData(int ConnectID ,NetMessage msg) ;	

		//主动关闭连接
		void DisposeOneConnection(int connectID ,DisconnectedCause cause) ;
	}	
}
