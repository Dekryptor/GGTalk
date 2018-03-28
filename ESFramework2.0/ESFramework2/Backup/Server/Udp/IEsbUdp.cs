using System;
using System.Threading ;
using System.Net ;
using System.Net.Sockets ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Server.Udp
{
	/// <summary>
	/// IEsbUdp ��ESFramework.Network�Ļ����϶�Udp�ķ�װ���ȿ����ڷ����Ҳ�����ڿͻ���	
	/// ʹ��NetMessage.Tag �����û���IPEndPoint
	/// ע��:udp����ͻ�����Ϊ64k(��65536�ֽ�)
	/// zhuweisky 2006.02.17
	/// </summary>
	public interface IEsbUdp :IUdpHookSender,IDisposable
	{
		bool AutoOnPublicIPAddress{set ;} //�Ƿ��Զ��ڼ�������������ַ�������Ϊ����ˣ�����Ϊtrue
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
