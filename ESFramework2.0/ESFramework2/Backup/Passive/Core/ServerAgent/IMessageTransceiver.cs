using System;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// IMessageTransceiver �ͻ���ʹ�õ���Ϣ�շ��������е���Ϣ���ͣ�������Ӧ�ظ�����ȡ��������IMessageTransceiver��
	/// zhuweisky 2006.06.02
	/// </summary>
	public interface IMessageTransceiver
	{
		/// <summary>
        /// CommitRequest �ύ��������ݡ�
        /// �����ʱ��Ȼû�лظ������׳���ʱ�쳣
		/// ���dataPriority != DataPriority.CanBeDiscarded ����checkRespondֻ��Ϊfalse
		/// </summary>     
		NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , bool checkRespond);	

		/// <summary>
		/// CommitRequest ��ȷָ������Ҫ����ServiceKeyΪresKey�Ļظ�
		/// </summary>	
		NetMessage CommitRequest(NetMessage requestMsg ,DataPriority dataPriority , int resKey);
	
		NetMessage CommitRequestToServer(NetMessage requestMsg ,DataPriority dataPriority , bool checkRespond);		

		IP2PChannelManager P2PChannelManager{set ;}
		IServerAgent       ServerAgent{set ;}
		IPassiveHelper     PassiveHelper{set ;}
		IResponseManager   ResponseManager{set ;}

		/// <summary>
		/// MustBeRelayDispersiveKeyScope ָ����Щ���͵�P2P��Ϣ���뾭����������ת��������ͨ��P2PChannel����		
		/// </summary>	
		DispersiveKeyScope MustBeRelayDispersiveKeyScope{set;} //Ԫ��ΪKeyScope
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

		#region IMessageTransceiver ��Ա

		public NetMessage CommitRequest(NetMessage requestMsg, DataPriority dataPriority, bool checkRespond)
		{
			//ͨ��IP2PChannel����P2PMessage
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
			//ͨ��IP2PChannel����P2PMessage
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
