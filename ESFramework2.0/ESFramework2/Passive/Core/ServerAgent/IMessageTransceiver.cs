using System;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// IMessageTransceiver 客户端使用的消息收发器，所有的消息发送（包括对应回复的提取）都经过IMessageTransceiver。
	/// zhuweisky 2006.06.02
	/// </summary>
	public interface IMessageTransceiver
	{
		/// <summary>
        /// CommitRequest 提交请求或数据。
        /// 如果超时仍然没有回复，则抛出超时异常
		/// 如果dataPriority != DataPriority.CanBeDiscarded ，则checkRespond只能为false
		/// </summary>     
		NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , bool checkRespond);	

		/// <summary>
		/// CommitRequest 明确指定定需要查找ServiceKey为resKey的回复
		/// </summary>	
		NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , int resKey);
	
		NetMessage CommitRequestToServer(NetMessage requestMsg ,DataPriority dataPriority , bool checkRespond);		

		IP2PChannelManager P2PChannelManager{set ;}
		IServerAgent       ServerAgent{set ;}
		IPassiveHelper     PassiveHelper{set ;}
		IResponseManager   ResponseManager{set ;}

		/// <summary>
		/// MustBeRelayDispersiveKeyScope 指明哪些类型的P2P消息必须经过服务器中转，而不是通过P2PChannel传送		
		/// </summary>	
		DispersiveKeyScope MustBeRelayDispersiveKeyScope{set;} //元素为KeyScope
	}

	public class MessageTransceiver :IMessageTransceiver
	{
		#region property
		#region P2PChannelManager
		private IP2PChannelManager p2PChannelManager = new EmptyP2PChannelManager() ; 
		public  IP2PChannelManager P2PChannelManager
		{
			set
			{
				this.p2PChannelManager = value ;
			}
		}
		#endregion
		
		#region ServerAgent
		private IServerAgent serverAgent = null ; 
		public  IServerAgent ServerAgent
		{
			set
			{
				this.serverAgent = value ;
			}
		}
		#endregion

		#region PassiveHelper
		private IPassiveHelper passiveHelper = null ; 
		public IPassiveHelper PassiveHelper
		{
			set
			{
				this.passiveHelper = value ;
			}
		}
		#endregion

		#region ResponseManager
		private IResponseManager responseManager = null ; 
		public IResponseManager ResponseManager
		{
			set
			{
				this.responseManager = value ;
			}
		}
		#endregion		

		#region MustBeRelayDispersiveKeyScope
		private DispersiveKeyScope mustBeRelayDispersiveKeyScope = new DispersiveKeyScope() ; 
		public  DispersiveKeyScope MustBeRelayDispersiveKeyScope
		{
			set
			{
				if(value == null)
				{
					this.mustBeRelayDispersiveKeyScope = new DispersiveKeyScope() ; 
				}
				else
				{
					this.mustBeRelayDispersiveKeyScope = value ;
				}
			}
		}
		#endregion

		#endregion

		#region IMessageTransceiver 成员

		public NetMessage CommitRequest(NetMessage requestMsg, DataPriority dataPriority, bool checkRespond)
		{
			//通过IP2PChannel发送P2PMessage
			if(this.passiveHelper.IsP2PMessage(requestMsg.Header.ServiceKey))
			{
				bool mustRelayed = this.mustBeRelayDispersiveKeyScope.Contains(requestMsg.Header.ServiceKey) ;
				if(! mustRelayed)
				{
					bool usable = this.p2PChannelManager.P2PChannelUsable(requestMsg.Header.DestUserID) ;
					if(usable)
					{
						this.p2PChannelManager.SendMessage(requestMsg.Header.DestUserID ,requestMsg) ;
						if(checkRespond)
						{
							return this.responseManager.PickupResponse(requestMsg.Header.ServiceKey ,requestMsg.Header.MessageID) ;
						}

						return null ;
					}
				}
			}
			
			return this.serverAgent.CommitRequest(requestMsg ,dataPriority ,checkRespond) ;			
		}	

		public NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , int resKey)
		{
			//通过IP2PChannel发送P2PMessage
			if(this.passiveHelper.IsP2PMessage(requestMsg.Header.ServiceKey))
			{
				bool mustRelayed = this.mustBeRelayDispersiveKeyScope.Contains(requestMsg.Header.ServiceKey) ;
				if(! mustRelayed)
				{
					bool usable = this.p2PChannelManager.P2PChannelUsable(requestMsg.Header.DestUserID) ;
					if(usable)
					{
						this.p2PChannelManager.SendMessage(requestMsg.Header.DestUserID ,requestMsg) ;
						return this.responseManager.PickupResponse(resKey ,requestMsg.Header.MessageID) ;						
					}
				}
			}
			
			return this.serverAgent.CommitRequest(requestMsg ,dataPriority ,resKey) ;		
		}
	
		public NetMessage CommitRequestToServer(NetMessage requestMsg ,DataPriority dataPriority , bool checkRespond)
		{
			return this.serverAgent.CommitRequest(requestMsg ,dataPriority ,checkRespond) ;	
		}

		#endregion
	}

}
