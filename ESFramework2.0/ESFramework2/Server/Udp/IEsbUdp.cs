using System;
using System.Threading ;
using System.Net ;
using System.Net.Sockets ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Udp
{
	/// <summary>
	/// IEsbUdp 在ESFramework.Network的基础上对Udp的封装。既可用于服务端也可用于客户端	
	/// 使用NetMessage.Tag 传递用户的IPEndPoint
	/// 注意:udp最大发送缓冲区为64k(即65536字节)
	/// zhuweisky 2006.02.17
	/// </summary>
	public interface IEsbUdp :IUdpHookSender,IDisposable
	{
		bool AutoOnPublicIPAddress{set ;} //是否自动在监听本机公网地址，如果作为服务端，则设为true
        int  Port { get;set;}
		IEsbLogger EsbLogger{set;}
		IContractHelper	   ContractHelper{set ;}  
		new IMessageDispatcher MessageDispatcher{set ;}		

		void Initialize() ;
		void Start() ;	
		void Stop() ;	
	
		event CbInvalidMsg InvalidMsgReceived ;
		event CbServiceCommitted ServiceCommitted ;
	}	

	public delegate void CbServiceCommitted(string userID ,NetMessage msg) ;
	public delegate void CbInvalidMsg(byte[] data ,IPEndPoint remoteIPE) ;	

	
	
}
