using System;
using System.Collections.Generic;
using System.Text;
using ESPlus.Application.CustomizeInfo.Server;
using ESPlus.Application.Basic.Server;
using ESPlus.Application.CustomizeInfo;
using ESFramework.Demos.Core;
using ESPlus.Application.Group.Server;
using ESFramework.Server.UserManagement;

namespace ESFramework.Demos.Server
{
    /// <summary>
    /// 自定义信息处理器
    /// </summary>
    public class CustomizeHandler : ICustomizeHandler 
    {
        /// <summary>
        /// 处理来自客户端的消息。
        /// </summary> 
        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
        }

        /// <summary>
        /// 处理来自客户端的同步调用请求。
        /// </summary>       
        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            if (informationType == InformationTypes.ClientCallServer)
            {
                string requestMessage = System.Text.Encoding.UTF8.GetString(info);
                string responseMessage = requestMessage + " 已经被服务端处理了！";
                return System.Text.Encoding.UTF8.GetBytes(responseMessage);
            }
            return null;
        }   
    }

    public class DefaultGroupManager : IGroupManager
    {
        #region Ctor
        public DefaultGroupManager() { }
        public DefaultGroupManager(IUserManager mgr)
        {
            this.userManager = mgr;
        }
        #endregion

        #region UserManager
        private IUserManager userManager;
        public IUserManager UserManager
        {
            set { userManager = value; }
        }
        #endregion

        public List<string> GetGroupmates(string userID)
        {
            return this.userManager.GetOnlineUserList();
        }

        public List<string> GetGroupMembers(string groupID)
        {
            if (groupID == "#0")
            {
                return this.userManager.GetOnlineUserList();
            }

            return new List<string>();
        }
    }

}
