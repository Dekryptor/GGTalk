using System;
using System.Collections.Generic ;

namespace ESFramework.Core
{
	/// <summary>
	/// ContainerStyleHook 封装Hook List，将多个hook当作一个整体
	/// </summary>	
	public class ContainerStyleHook :INetMessageHook
	{
		#region property
        private IList<INetMessageHook> hookList = new List<INetMessageHook>();
        public IList<INetMessageHook> HookList
		{
			set
			{
				this.hookList = value ;
			}
		}

		#region Enabled
		private bool enabled = true ; 
		public bool Enabled
		{
			get
			{
				return this.enabled ;
			}
			set
			{
				this.enabled = value ;
			}
		}
		#endregion
		
		#region HookP2pMessage 通常服务端设置为false ，而客户端设置为true
		private bool hookP2pMessage = true ; 
		public bool HookP2pMessage
		{
			get
			{
				return this.hookP2pMessage ;
			}
			set
			{
				this.hookP2pMessage = value ;
			}
		}
		#endregion

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

		#endregion

		#region INetMessageHook 成员
		public NetMessage CaptureReceivedMsg(NetMessage msg)
		{	
			if(! this.enabled)
			{
				return msg ;
			}

			if(msg == null)
			{
				return null ;
			}

			if(this.contractHelper.IsP2PMessage(msg.Header.ServiceKey))
			{
				if(! this.hookP2pMessage)
				{
					return msg ;
				}
			}		

			NetMessage result = msg ;
			for(int i=0 ;i<this.hookList.Count ;i++)
			{
				INetMessageHook hook = this.hookList[i] ;
				result = hook.CaptureReceivedMsg(result) ;
			}

			return result ;
		}

		public NetMessage CaptureBeforeSendMsg(NetMessage msg)
		{
			if(! this.enabled)
			{
				return msg ;
			}

			if(msg == null)
			{
				return null ;
			}

			if(this.contractHelper.IsP2PMessage(msg.Header.ServiceKey))
			{
				if(! this.hookP2pMessage)
				{
					return msg ;
				}
			}

			NetMessage result = msg ;
			for(int i=this.hookList.Count-1 ;i>=0 ;i-- )
			{
				INetMessageHook hook = this.hookList[i] ;
				result = hook.CaptureBeforeSendMsg(result) ;
			}

			return result ;
		}				

		public int[] ServiceKeyHookedList
		{
			set
			{
				// TODO:  添加 EsbNetMessageHook.ServiceKeyHookedList setter 实现
			}
		}

		public int[] ServiceKeyNotHookedList
		{
			set
			{
				// TODO:  添加 EsbNetMessageHook.ServiceKeyNotHookedList setter 实现
			}
		}

		public ServiceKeyHookedStyle ServiceKeyHookedStyle
		{
			set
			{
				// TODO:  添加 EsbNetMessageHook.ServiceKeyHookedStyle setter 实现
			}
		}

		#endregion
	}
	
}
