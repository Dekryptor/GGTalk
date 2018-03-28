using System;
using System.Net.Sockets ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Passive.Tcp
{
	/// <summary>
	/// ITcpPassive ITcp 用于服务端，而ITcpPassive用于客户端。
	/// ITcpPassive 封装了数据接收线程，完全向客户端隐藏了网络通信细节。
	/// Client所有的通过网络发送/接收数据最好都通过ITcpPassive进行	
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