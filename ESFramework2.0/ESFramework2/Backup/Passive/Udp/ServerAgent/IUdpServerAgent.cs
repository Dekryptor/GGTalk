using System;
using System.Net ;
using ESFramework.Server.Udp ;
using ESFramework.Core;

namespace ESFramework.Passive.Udp
{
	/// <summary>
	/// IUdpServerAgent ͬITcpServerAgent���ں���IEsbUdp���
	/// zhuweisky 2005.03.10
	/// </summary>
	public interface IUdpServerAgent :IServerAgent ,IDisposable
	{
		IServerAgentHelper ServerAgentHelper{set ;}

		IPEndPoint ServerIPE{set ;} 
		IUdpHookSender UdpHookSender{get ;}
		
		//����ת�Ƹ�EsbUdp
		int  Port{set ;}			
	
		event CbInvalidMsg InvalidMsgReceived ;
		event CbServiceCommitted ServiceCommitted ;
	}
}
