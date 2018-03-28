using System;
using System.Net ;
using ESFramework.Server.Udp ;
using ESFramework.Core;

namespace ESFramework.Passive.Udp
{
	/// <summary>
	/// UdpServerAgent 的摘要说明。
	/// </summary>
	public class UdpServerAgent :IUdpServerAgent 
	{
		private IEsbUdp esbUdp = new EsbUdp() ;		

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
	
		#region IUdpServerAgent 成员
		public void Initialize()
		{
			this.esbUdp.ContractHelper    = this.serverAgentHelper.ContractHelper ;
			this.esbUdp.EsbLogger         = this.serverAgentHelper.EsbLogger ;			
			this.esbUdp.MessageDispatcher = this.serverAgentHelper.MessageDispatcher ;
			
			this.esbUdp.Initialize() ;			
			this.esbUdp.Start() ;			
		}			

		#region ->esbUdp
		public int Port
		{
			set
			{
				this.esbUdp.Port = value ;
			}
		}	
		

		public event CbInvalidMsg InvalidMsgReceived
		{
			add
			{
				this.esbUdp.InvalidMsgReceived += value ;
			}
			remove
			{
				this.esbUdp.InvalidMsgReceived -= value ;
			}
		}

		public event CbServiceCommitted ServiceCommitted
		{
			add
			{
				this.esbUdp.ServiceCommitted += value ;
			}
			remove
			{
				this.esbUdp.ServiceCommitted -= value ;
			}
		}
		#endregion

		#endregion

		#region IUdpServerAgent 成员
		private IPEndPoint serverIPE = null ;
		public  IPEndPoint ServerIPE
		{
			set
			{
				this.serverIPE = value ;
			}
		}

		public IUdpHookSender UdpHookSender
		{
			get
			{
				return this.esbUdp ;
			}
		}

		public NetMessage CommitRequest(NetMessage requestMsg, DataPriority dataPriority, bool checkRespond)
		{
			this.esbUdp.HookAndSendNetMessage(this.serverIPE ,requestMsg);
			if(checkRespond)
			{
				return this.serverAgentHelper.ResponseManager.PickupResponse(requestMsg.Header.ServiceKey, requestMsg.Header.MessageID);
			}

			return null ;
		}	
		
		public NetMessage CommitRequest(NetMessage requestMsg, DataPriority dataPriority, int resKey)
		{	
			this.esbUdp.HookAndSendNetMessage(this.serverIPE ,requestMsg);		

			return this.serverAgentHelper.ResponseManager.PickupResponse(resKey, requestMsg.Header.MessageID);
		}
		#endregion

		#region IDisposable 成员

		public void Dispose()
		{
			this.esbUdp.Stop() ;
		}

		#endregion
	}
}
