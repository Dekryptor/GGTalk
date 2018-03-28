using System;
using System.Collections.Generic;
using ESBasic;
using ESFramework.Core;

namespace ESFramework.Passive
{
	/// <summary>
	/// SimplePassiveDataDealer �򵥵�ͨ��SelectiveDataDealer������������Ϣ��
	/// �ͻ���ʹ��SimplePassiveDataDealer�������н��յ�����Ϣ���������Է������������ͻ���
	/// </summary>
    public class SimplePassiveProcesser : IMessageProcesser
	{
        public SimplePassiveProcesser()
		{			
		}

		#region Property
		#region DataDealerFactory
        private ProcesserFactory processerFactory = null;
        public ProcesserFactory ProcesserFactory
		{
			set
			{
                this.processerFactory = value;
			}
		}
		#endregion

		#region AllDataDealer
        private SelectiveProcesser allDataDealer = null;
        public SelectiveProcesser AllDataDealer
		{
			set
			{
				this.allDataDealer = value ;
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

		#region PushKeyDispersiveKeyScope
		private DispersiveKeyScope pushKeyDispersiveKeyScope = new DispersiveKeyScope() ; 
		public  DispersiveKeyScope PushKeyDispersiveKeyScope
		{
			get
			{
				return this.pushKeyDispersiveKeyScope ;
			}
			set
			{
				if(value == null)
				{
					this.pushKeyDispersiveKeyScope = new DispersiveKeyScope() ; 
				}
				else
				{
					this.pushKeyDispersiveKeyScope = value ;
				}
			}
		}
		#endregion
		#endregion

		#region AddPushKey
        private IList<int> otherPushKeyList = new List<int>();
		public void AddPushKey(int key)
		{
			this.otherPushKeyList.Add(key) ;
		}
		#endregion

		#region IMessageProcesser ��Ա

        public NetMessage ProcessMessage(NetMessage reqMsg)
		{
            bool push = this.NeedPush(reqMsg.Header.ServiceKey);			
		
			if(push)
			{
				this.responseManager.PushResponse(reqMsg) ;
				return null ;
			}
			else
			{
				if(this.processerFactory == null)
				{
                    return this.allDataDealer.ProcessMessage(reqMsg);
				}
				else
				{
                    IMessageProcesser dealer = this.processerFactory.CreateProcesser(reqMsg.Header.ServiceKey, -1);
                    return dealer.ProcessMessage(reqMsg);
				}
			}
		}

        #region NeedPush
        /// <summary>
        /// NeedPush �Ƿ���Ҫ����Ϣ����IResponseManager
        /// </summary>        
        private bool NeedPush(int serviceKey)
        {
            if (this.pushKeyDispersiveKeyScope.Contains(serviceKey))
            {
                return true;
            }

            foreach (int key in this.otherPushKeyList)
            {
                if (key == serviceKey)
                {
                    return true;
                }
            }

            return false;
        } 
        #endregion
		#endregion
	}
}
