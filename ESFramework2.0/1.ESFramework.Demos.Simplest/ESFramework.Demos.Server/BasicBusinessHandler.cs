using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Application.Basic.Server;

namespace ESFramework.Demos.Server
{
    /// <summary>
    /// ������������������֤��½���û���
    /// </summary>
    public class BasicHandler : IBasicHandler
    {   
        /// <summary>
        /// �˴���֤�û����˺ź����롣����true��ʾͨ����֤��
        /// </summary>  
        public bool VerifyUser(string systemToken, string userID, string password, out string failureCause)
        {
            failureCause = "";
            return true;
        }
    }
}
