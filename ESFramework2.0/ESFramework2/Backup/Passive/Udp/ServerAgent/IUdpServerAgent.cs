using System;
using System.Net ;
using ESFramework.Server.Udp ;
using ESFramework.Core;

namespace ESFramework.Passive.Udp
{
	/// <summary>
	/// IUdpServerAgent 同ITcpServerAgent。内含了IEsbUdp组件
	/// zhuweisky 2005.03.10
	/// </summary>
	public interface IUdpServerAgent :IServerAgent ,IDisposable
	{
		IServerAgentHelper ServerAgentHelper{set ;}

		IPEndPoint ServerIPE{set ;} 
		IUdpHookSender UdpHookSender{get ;}
		
		//以下转移给EsbUdp
		int  Port{set ;}			
	
		event CbInvalidMsg InvalidMsgReceived ;
		event CbServiceCommitted ServiceCommitted ;
	}
}
