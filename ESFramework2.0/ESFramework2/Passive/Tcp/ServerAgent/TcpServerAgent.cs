using System;
using System.Net.Sockets ;
using ESFramework.Passive ;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Passive.Tcp
{
	/// <summary>
	/// TcpServerAgent ITcpServerAgent的参考实现。
	/// </summary>
	public class TcpServerAgent :ITcpServerAgent
	{      
		private ITcpPassive tcpPassive = new TcpPassive() ;			

		#region property
		public NetworkStream NetworkStream
		{
			set
			{	
				this.tcpPassive.NetworkStream = value ;             
			}
		}	

		#region ServerAgentHelper
		private IServerAgentHelper serverAgentHelper = null ; 
		public IServerAgentHelper ServerAgentHelper
		{
			set
			{
				this.serverAgentHelper = value ;
			}
		}
		#endregion
		
		#endregion

		#region ITcpServerAgent 成员

		#region CommitRequest
		public NetMessage CommitRequest(NetMessage requestMsg, DataPriority dataPriority, bool checkRespond)
		{	
			this.tcpPassive.HookAndSendMessage(requestMsg, dataPriority);
		
			if((! checkRespond) || (dataPriority == DataPriority.CanBeDiscarded))
			{				
				return null ;
			}

			return this.serverAgentHelper.ResponseManager.PickupResponse(requestMsg.Header.ServiceKey, requestMsg.Header.MessageID);
		}

		public NetMessage CommitRequest(NetMessage requestMsg, DataPriority dataPriority, int resKey)
		{	
			this.tcpPassive.HookAndSendMessage(requestMsg, dataPriority);			

			return this.serverAgentHelper.ResponseManager.PickupResponse(resKey, requestMsg.Header.MessageID);
		}
		
		#endregion		

		#region event
		public event CbSimple ConnectionInterrupted 
		{
			add
			{
				this.tcpPassive.ConnectionInterrupted += value;
			}
			remove
			{
				this.tcpPassive.ConnectionInterrupted -= value;
			}
		}

		public event CbStream DataDiscarded
		{
			add
			{
				this.tcpPassive.DataDiscarded += value;
			}
			remove
			{
				this.tcpPassive.DataDiscarded -= value;
			}
		}

		public event CbSimple DataLacked
		{
			add
			{
				this.tcpPassive.DataLacked += value;
			}
			remove
			{
				this.tcpPassive.DataLacked -= value;
			}
		}
		#endregion

		#region Initialize
		public void Initialize()
		{			
			this.tcpPassive.ContractHelper    = this.serverAgentHelper.ContractHelper ;
			this.tcpPassive.EsbLogger         = this.serverAgentHelper.EsbLogger ;			
			this.tcpPassive.MessageDispatcher = this.serverAgentHelper.MessageDispatcher ;

			this.tcpPassive.Initialize() ;			
		}		
		#endregion

		#region ClearQueue
		public void ClearQueue(DataPriority queueType)
		{
			this.tcpPassive.ClearQueue(queueType) ;
		}
		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			this.tcpPassive.Dispose();
		}

		#endregion


		#endregion
	}
}
