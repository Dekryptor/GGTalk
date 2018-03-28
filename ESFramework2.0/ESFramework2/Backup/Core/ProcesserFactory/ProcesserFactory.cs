using System;
using System.Collections.Generic;

namespace ESFramework.Core
{
	/// <summary>
    /// ProcesserFactory 基于ProcesserBag来灵活装配各种处理器。
	/// </summary>
	public class ProcesserFactory :IProcesserFactory
	{
        public ProcesserFactory()
		{
        }

        #region ForeignProcesser
        private IMessageProcesser foreignProcesser ;//ESFramework.Architecture.FourTier.ForeignDealer
        public IMessageProcesser ForeignProcesser
		{
			set
			{
                this.foreignProcesser = value;
			}
		}
		#endregion

        #region ProcesserBagList
        private IList<ProcesserBag> processerBagList = new List<ProcesserBag>(); 
        public IList<ProcesserBag> ProcesserBagList
		{
			set
			{
                this.processerBagList = value;
			}
		}
		#endregion	

		#region ContractHelper
		private IContractHelper contractHelper = null ; 
		public IContractHelper ContractHelper
		{		
			set
			{
				this.contractHelper = value ;
			}
		}
		#endregion

		#region IProcesserFactory 成员

        public IMessageProcesser CreateProcesser(int serviceKey, int serverTypeKey)
		{
			if(serverTypeKey >0)
			{
				if(this.contractHelper.ServerType != serverTypeKey)
				{
                    return this.foreignProcesser;
				}
			}

            foreach (ProcesserBag bag in this.processerBagList)
			{
				if(bag.Contains(serviceKey))
				{
                    return bag.GetProcesser(serviceKey);
				}
			}

			return null;
		}

		#endregion
	}
}
