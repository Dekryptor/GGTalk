using System;
using System.Collections.Generic;
using System.Text;

namespace ESFramework.Core
{
    public class ContainerStyleSpy : INetMessageSpy
    {
        #region SpyList
        private IList<INetMessageSpy> spyList = new List<INetMessageSpy>();
        public IList<INetMessageSpy> SpyList
        {
            set { spyList = value; }
        } 
        #endregion

        #region INetMessageSpy ≥…‘±

        #region Enabled
        private bool enabled = true;
        public bool Enabled
        {
            set { this.enabled = value; }
            get { return this.enabled; }
        } 
        #endregion

        public bool SpyReceivedMsg(NetMessage msg)
        {
            if (! this.enabled)
            {
                return true;
            }

            foreach (INetMessageSpy spy in this.spyList)
            {
                bool keepOn = spy.SpyReceivedMsg(msg);
                if (! keepOn)
                {
                    return false;
                }
            }

            return true;
        }

        public bool SpyToBeSendedMsg(NetMessage msg)
        {
            if (!this.enabled)
            {
                return true;
            }

            foreach (INetMessageSpy spy in this.spyList)
            {
                bool keepOn = spy.SpyToBeSendedMsg(msg);
                if (!keepOn)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
