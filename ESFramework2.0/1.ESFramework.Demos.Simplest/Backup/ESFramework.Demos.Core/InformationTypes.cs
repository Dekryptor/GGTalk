using System;
using System.Collections.Generic;
using System.Text;

namespace ESFramework.Demos.Core
{
    /// <summary>
    /// 自定义信息的类型
    /// </summary>
    public static class InformationTypes
    {
        /// <summary>
        /// 聊天信息
        /// </summary>
        public const int Chat = 0;

        /// <summary>
        /// 客户端同步调用服务端
        /// </summary>
        public const int ClientCallServer = 100;

        /// <summary>
        /// 服务端发送消息到客户端
        /// </summary>
        public const int ServerSendToClient = 101;
        /// <summary>
        /// 服务端同步调用客户端
        /// </summary>
        public const int ServerCallClient = 102;


        /// <summary>
        /// 客户端同步调用客户端
        /// </summary>
        public const int ClientCallClient = 104;

        public const int SendBlob = 105;


        /// <summary>
        /// 广播消息
        /// </summary>
        public const int Broadcast = 106;


    }
}
