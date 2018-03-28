using System;
using System.Collections.Generic;
using System.Text;

namespace ESFramework.Core
{
    /// <summary>
    /// ITcpHookSender TcpHookSender实现了该接口
    /// </summary>
    public interface ITcpHookSender : IHookSender
    {
        void HookAndSendNetMessage(int connectID, NetMessage msg);
    }
}
