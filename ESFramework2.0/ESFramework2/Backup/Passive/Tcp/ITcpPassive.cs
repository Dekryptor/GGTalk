using System;
using System.Net.Sockets ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Passive.Tcp
{
	/// <summary>
	/// ITcpPassive ITcp ���ڷ���ˣ���ITcpPassive���ڿͻ��ˡ�
	/// ITcpPassive ��װ�����ݽ����̣߳���ȫ��ͻ�������������ͨ��ϸ�ڡ�
	/// Client���е�ͨ�����緢��/����������ö�ͨ��ITcpPassive����	
	/// zhuweisky 2006.01.12
	/// </summary>
	public interface ITcpPassive :IDisposable
	{
		NetworkStream	   NetworkStream{set ;}
		
		IContractHelper    ContractHelper{set ;}		
		IMessageDispatcher MessageDispatcher{set;}
		void Initialize() ;			
		void HookAndSendMessage(NetMessage msg, DataPriority dataPriority);

		IEsbLogger EsbLogger{set ;}

		void ClearQueue(DataPriority queueType) ;

		event CbStream DataDiscarded;
		event CbSimple    DataLacked;
		event CbSimple        ConnectionInterrupted ;
	}	

}