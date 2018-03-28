using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace ESFramework.Network.Extend
{
	/// <summary>
	/// MessageTypeManager �����е�IMessageType����������
	/// </summary>
	public class MessageTypeRoomManager :IServiceKeyNameMatcher
	{
		private Hashtable htKeyName = new Hashtable() ; //ServiceKey �� Name
        public MessageTypeRoomManager()
		{			
		}

		#region MessageTypeList
        private IList<IMessageTypeRoom> messageTypeRoomList = new List<IMessageTypeRoom>(); //Ԫ��Ϊ IMessageType
        public IList<IMessageTypeRoom> MessageTypeRoomList
		{
			set
			{
				if(value != null)
				{
                    this.messageTypeRoomList = value;
				}
				else
				{
                    this.messageTypeRoomList = new List<IMessageTypeRoom>();
				}
			}
		}
		#endregion
		
		#region Conflict
		/// <summary>
		/// Conflict �жϸ���MessageType��ServiceKey�����Ƿ�����ͻ
		/// </summary>		
		public bool Conflict()
		{
			if(this.messageTypeRoomList.Count <2)
			{
				return false ;
			}

            for (int i = 0; i < this.messageTypeRoomList.Count - 1; i++)
			{
                IMessageTypeRoom outterMsgType = this.messageTypeRoomList[i];
                for (int j = i + 1; j < this.messageTypeRoomList.Count; j++)
				{
                    IMessageTypeRoom innerMsgType = this.messageTypeRoomList[j];
					if(outterMsgType.KeyScope.Intersect(innerMsgType.KeyScope))
					{
						return true ;
					}
				}
			}

			return false ;
		}
		#endregion

		#region PushKeyList
		/// <summary>
		/// PushKeyList ָʾ�ͻ�����Ҫ����Щ���͵���ϢPush��IResponseManager
		/// </summary>
		public IList<int> PushKeyList
		{
			get
			{
                IList<int> list = new List<int>();
				foreach(IMessageTypeRoom msgTypeRoom in this.messageTypeRoomList)
				{
                    foreach (int pushKey in msgTypeRoom.PushKeyList)
					{
						list.Add(pushKey) ;
					}
				}

				return list ;
			}
		}
		#endregion
		
		#region IServiceKeyNameMatcher ��Ա
		/// <summary>
		/// GetServiceName ͨ������ؼ��ֻ�ȡ��������
		/// </summary>		
		public string GetServiceName(int serviceKey)
		{
			if(this.htKeyName[serviceKey] != null)
			{
				return this.htKeyName[serviceKey].ToString();
			}

			foreach(IMessageTypeRoom msgType in this.messageTypeRoomList)
			{
				if(msgType.KeyScope.Contains(serviceKey))
				{
					#region GetPropertyName
					Type curType = msgType.GetType() ;
					PropertyInfo[] pros = curType.GetProperties(BindingFlags.Default |BindingFlags.Public | BindingFlags.Instance) ;
					foreach(PropertyInfo info in pros)
					{
						if((!info.CanWrite) || (info.PropertyType != typeof(int)) || (info.Name == "StartKey"))
						{
							continue ;
						}

						int proVal =  (int)info.GetValue(msgType ,null) ;
						if(proVal == serviceKey)
						{
							string serviceName = curType.FullName + "." + info.Name ;
							lock(this.htKeyName)
							{
								this.htKeyName.Remove(serviceKey) ;
								this.htKeyName.Add(serviceKey ,serviceName) ;
							}

							return serviceName ;
						}						
					}
					#endregion
				}
			}

			return serviceKey.ToString();
		}

		#endregion
	}
}
