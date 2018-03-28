using System;
using System.Collections.Generic;
using System.Text;

namespace ESFramework.Core
{
    public class MessageDispatcher : IMessageDispatcher
    {
        #region property
        #region Logger
        private IEsbLogger esbLogger = new EmptyEsbLogger();
        public IEsbLogger EsbLogger
        {
            set
            {
                if (value != null)
                {
                    this.esbLogger = value;
                }
            }
        }
        #endregion

        #region NakeDispatcher
        private INakeDispatcher nakeDispatcher;
        public INakeDispatcher NakeDispatcher
        {
            set
            {
                this.nakeDispatcher = value;
            }
        }
        #endregion

        #region NetMessageHook
        private INetMessageHook netMessageHook = new EmptyNetMessageHook();
        public INetMessageHook NetMessageHook
        {
            set
            {
                this.netMessageHook = value;
            }
        }
        #endregion

        #region InnerMessageSpy
        private IInnerMessageSpy innerMessageSpy = new EmptyInnerMessageSpy();
        public IInnerMessageSpy InnerMessageSpy
        {
            set
            {
                if (value != null)
                {
                    this.innerMessageSpy = value;
                }

            }
        }
        #endregion

        #region GatewayMessageSpy
        private IGatewayMessageSpy gatewayMessageSpy = new EmptyGatewayNetMessageSpy();
        public IGatewayMessageSpy GatewayMessageSpy
        {
            set
            {
                if (value != null)
                {
                    this.gatewayMessageSpy = value;
                }
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

        public event CbNetMessage MessageReceived;
        #endregion

        #region IMessageDispatcher ≥…‘±

        public NetMessage DispatchMessage(NetMessage reqMsg)
        {
            try
            {
                if (this.MessageReceived != null)
                {
                    this.MessageReceived(reqMsg);
                }

                bool valid = this.gatewayMessageSpy.SpyReceivedMsg(reqMsg);
                if (!valid)
                {
                    return null;
                }

                NetMessage msgHooked = this.netMessageHook.CaptureReceivedMsg(reqMsg);
                valid = this.innerMessageSpy.SpyReceivedMsg(msgHooked);
                if (!valid)
                {
                    return null;
                }

                NetMessage resMsg = this.nakeDispatcher.DispatchMessage(msgHooked);
                if (resMsg == null)
                {
                    return null;
                }

                return this.BeforeSendMessage(resMsg);
            }
            catch (Exception ee)
            {
                this.esbLogger.Log(ee.GetType().ToString(), ee.Message, "ESFramework.Network.MessageDispatcher.DispatchMessage", ErrorLevel.High);
                return null;
            }
        }

        public NetMessage BeforeSendMessage(NetMessage msg)
        {
            bool valid = this.innerMessageSpy.SpyToBeSendedMsg(msg);
            if (!valid)
            {
                return null;
            }
            NetMessage msgHooked = this.netMessageHook.CaptureBeforeSendMsg(msg);
            valid = this.gatewayMessageSpy.SpyToBeSendedMsg(msgHooked);

            if (!valid)
            {
                return null;
            }

            return msgHooked;
        }


        #endregion
    }
}
