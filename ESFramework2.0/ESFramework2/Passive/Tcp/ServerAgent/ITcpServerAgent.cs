using System;
using System.Net.Sockets;
using ESFramework.Passive ;
using ESBasic;

namespace ESFramework.Passive.Tcp
{
	/// <summary>
	/// ITcpServerAgent ���ڽ��ͻ���(tcp����/��Ӧ)����ģ��ɱ��ط������ã�
	/// ESFramework.Network.Passive.Tcp �ռ�ĺ��Ľӿڣ��ں���ITcpPassive���
	/// ʹ�ô������ռ䣬����Ϣͷ�е�RandomNum�ǳ���Ҫ������ƥ��Request/Response
	/// ��ServiceType.Function���͵�����֧�����!
	/// zhuweisky 2006.02.08
	/// </summary>
	public interface ITcpServerAgent :IServerAgent ,IDisposable 
	{		
		IServerAgentHelper ServerAgentHelper{set ;}
		NetworkStream     NetworkStream { set;}			

		/// <summary>
		/// ����Ŷӵȴ����͵���Ϣ
		/// </summary>		
		void ClearQueue(DataPriority queueType) ;		

		event CbStream DataDiscarded;
		event CbSimple    DataLacked;
		event CbSimple        ConnectionInterrupted ;
	}
}
