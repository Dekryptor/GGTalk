using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Application.Basic.Server;

namespace ESFramework.Demos.Server
{
    /// <summary>
    /// 基础处理器，用于验证登陆的用户。
    /// </summary>
    public class BasicHandler : IBasicHandler
    {   
        /// <summary>
        /// 此处验证用户的账号和密码。返回true表示通过验证。
        /// </summary>  
        public bool VerifyUser(string systemToken, string userID, string password, out string failureCause)
        {
            failureCause = "";
            return true;
        }
    }
}
