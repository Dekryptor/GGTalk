using System;
using ESFramework.Core;

namespace ESFramework.Server.Tcp
{
	/// <summary>
	/// ITcpStreamDispatcher 是IMessageDispatcher的简单包装，附加了针对Tcp的专门验证消息的环节
	/// </summary>
	public interface ITcpStreamDispatcher
	{
		/// <summary>
		/// DealRequestData 不允许抛出异常		
		/// </summary>		
		NetMessage DealRequestData(RoundedMessage reqMsg ,ref bool closeConnection) ;		
	}	

	#region TcpStreamDispatcher
	public class TcpStreamDispatcher :ITcpStreamDispatcher 
	{
		#region property	
		#region ContractHelper
		private IContractHelper contractHelper = null ;
		public  IContractHelper ContractHelper
		{
			set
			{
				this.contractHelper = value ;
			}
		}
		#endregion		

		#region MessageDispatcher
		private IMessageDispatcher messageDispatcher = null ; 
		public  IMessageDispatcher MessageDispatcher
		{
			set
			{
				this.messageDispatcher = value ;
			}
		}
		#endregion
		#endregion		

		public NetMessage DealRequestData(RoundedMessage reqMsg, ref bool closeConnection)
		{
			closeConnection = false ;

			#region 验证消息
			if(reqMsg.IsFirstMessage)
			{
				if(! this.contractHelper.VerifyFirstMessage(reqMsg))
				{
					closeConnection = true ;
					return null ;
				}
			}
			else
			{
				if(! this.contractHelper.VerifyOtherMessage(reqMsg))
				{
					closeConnection = true ;
					return null ;
				}
			}
			#endregion

			return this.messageDispatcher.DispatchMessage(reqMsg) ;
		}		
	}
	#endregion
	
}
