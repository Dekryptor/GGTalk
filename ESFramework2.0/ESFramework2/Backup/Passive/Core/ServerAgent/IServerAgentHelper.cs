using System;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// IBaseServerAgent 用于辅助构建TcpServerAgent和UdpServerAgent。
	/// </summary>
	public interface IServerAgentHelper
	{
		IEsbLogger		   EsbLogger{set ; get ;}
		IContractHelper    ContractHelper{set ; get ;}
		INetMessageHook    NetMessageHook {set ; get ;}
		IMessageDispatcher MessageDispatcher{set ; get ;}	
		IResponseManager   ResponseManager{set ;get ;}			
	}	

	public class ServerAgentHelper :IServerAgentHelper 
	{
		#region EsbLogger
		private IEsbLogger esbLogger = null ; 
		public IEsbLogger EsbLogger
		{
			get
			{
				return this.esbLogger ;
			}
			set
			{
				this.esbLogger = value ;
			}
		}
		#endregion
		
		#region ContractHelper
		private IContractHelper contractHelper = null ; 
		public IContractHelper ContractHelper
		{
			get
			{
				return this.contractHelper ;
			}
			set
			{
				this.contractHelper = value ;
			}
		}
		#endregion
		
		#region NetMessageHook
		private INetMessageHook netMessageHook = null ; 
		public INetMessageHook NetMessageHook
		{
			get
			{
				return this.netMessageHook ;
			}
			set
			{
				this.netMessageHook = value ;
			}
		}
		#endregion
		
		#region MessageDispatcher
		private IMessageDispatcher messageDispatcher = null ; 
		public IMessageDispatcher MessageDispatcher
		{
			get
			{
				return this.messageDispatcher ;
			}
			set
			{
				this.messageDispatcher = value ;
			}
		}
		#endregion
		
		#region ResponseManager
		private IResponseManager responseManager = null ; 
		public IResponseManager ResponseManager
		{
			get
			{
				return this.responseManager ;
			}
			set
			{
				this.responseManager = value ;
			}
		}
		#endregion

	}
}
