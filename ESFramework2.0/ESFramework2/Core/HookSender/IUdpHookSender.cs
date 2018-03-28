using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ESFramework.Core
{
    /// <summary>
    /// IUdpHookSender IEsbUdp½Ó¿Ú´ÓIUdpHookSender¼Ì³Ð
    /// </summary>
    public interface IUdpHookSender : IHookSender
    {
        void HookAndSendNetMessage(IPEndPoint clientIpe, NetMessage msg);
    }
}
