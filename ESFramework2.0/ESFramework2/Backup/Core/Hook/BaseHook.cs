using System;

namespace ESFramework.Core
{
	/// <summary>
	/// BaseHook ���е�Hook�Ӵ˻���̳У�ֻҪʵ����Ϣ�任�������鷽�����ɡ�
	/// </summary>
	public abstract class BaseHook :INetMessageHook
	{
		public BaseHook()
		{			
		}

		protected abstract NetMessage DoTranslateReceived(NetMessage msg) ;
		protected abstract NetMessage DoTranslateBeforeSend(NetMessage msg) ;
	
		#region INetMessageHook ��Ա

		#region CaptureReceivedMsg
		public NetMessage CaptureReceivedMsg(NetMessage msg)
		{
			if(!this.enabled)
			{
				return msg ;
			}

			if(! this.Capture(msg.Header.ServiceKey))
			{
				return msg ;
			}					

			if(msg.Body == null)
			{
				return msg ;
			}		

			return this.CaptureReceivedMsg(msg) ;
		}
		#endregion

		#region CaptureBeforeSendMsg
		public NetMessage CaptureBeforeSendMsg(NetMessage msg)
		{
			if(!this.enabled)
			{
				return msg ;
			}

			if(! this.Capture(msg.Header.ServiceKey))
			{
				return msg ;
			}					

			if(msg.Body == null)
			{
				return msg ;
			}			

			return this.DoTranslateBeforeSend(msg) ;
		}

		private bool Capture(int serviceKey)
		{
			if(this.serviceKeyHookedStyle == ServiceKeyHookedStyle.Default)
			{
				return true ;
			}

			if(this.serviceKeyHookedStyle == ServiceKeyHookedStyle.HookedList)
			{
				foreach(int key in this.serviceKeyHookedList)
				{
					if(key == serviceKey)
					{
						return true ;
					}
				}

				return false ;
			}
			else
			{
				foreach(int key in this.serviceKeyNotHookedList)
				{
					if(key == serviceKey)
					{
						return false ;
					}
				}

				return true ;
			}
		}
		#endregion

		#region Enabled
		private bool enabled = true ; 
		public  bool Enabled
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
		
		#region ServiceKeyHookedList
		private int[] serviceKeyHookedList = new int[0] ;
		public  int[] ServiceKeyHookedList
		{
			set
			{
				this.serviceKeyHookedList = value ;
			}
		}

		private int[] serviceKeyNotHookedList = new int[0] ;
		public  int[] ServiceKeyNotHookedList
		{
			set
			{
				this.serviceKeyNotHookedList = value ;
			}
		}

		private ServiceKeyHookedStyle serviceKeyHookedStyle = ServiceKeyHookedStyle.Default ;
		public  ServiceKeyHookedStyle ServiceKeyHookedStyle
		{
			set
			{
				this.serviceKeyHookedStyle = value ;
			}
		}
		#endregion
		#endregion
	}
}
