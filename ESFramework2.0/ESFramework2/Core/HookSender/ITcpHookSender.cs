using System;
using System.Collections.Generic;
using System.Text;

namespace ESFramework.Core
{
    /// <summary>
    /// ITcpHookSender TcpHookSenderʵ���˸ýӿ�
    /// </summary>
    public interface ITcpHookSender : IHookSender
    {
        void HookAndSendNetMessage(int connectID, NetMessage msg);
    }
}
