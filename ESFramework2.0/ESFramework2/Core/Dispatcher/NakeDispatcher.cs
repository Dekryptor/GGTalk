using System;
using System.Collections.Generic;
using System.Text;

namespace ESFramework.Core
{
    public class NakeDispatcher : INakeDispatcher
    {
        #region ProcesserFactory
        private IProcesserFactory processerFactory = null;
        public IProcesserFactory ProcesserFactory
        {
            set
            {
                this.processerFactory = value;
            }
        }
        #endregion

        #region ContractHelper
        private IContractHelper contractHelper = null;
        public IContractHelper ContractHelper
        {
            set
            {
                this.contractHelper = value;
            }
        }
        #endregion

        #region INakeDispatcher ≥…‘±
        public NetMessage DispatchMessage(NetMessage msg)
        {
            IMessageProcesser dealer = this.processerFactory.CreateProcesser(msg.Header.ServiceKey, msg.Header.TypeKey);
            if (dealer == null)
            {
                return null;
            }

            return dealer.ProcessMessage(msg);
        }
        #endregion
    }
}
