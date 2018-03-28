using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESPlus.Widgets;
using ESPlus.Rapid;
using ESPlus.Application.CustomizeInfo.Server;
using ESFramework;
using ESFramework.Server.UserManagement;
using ESPlus.Core;
using ESPlus.Application.Friends.Server;
/*
 * 本demo采用的是ESFramework的免费版本，若想获取ESFramework其它版本，请联系 www.oraycn.com 或 QQ：168757008。
 * 
 */
namespace ESFramework.Demos.Server
{
    static class Program
    {
        private static ESPlus.Rapid.IRapidServerEngine RapidServerEngine = ESPlus.Rapid.RapidEngineFactory.CreateServerEngine();

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //如果是其它类型的授权用户，请使用下面的语句设定正确的授权用户ID和密码。              
                ESPlus.GlobalUtil.SetAuthorizedUser("FreeUser", "");

                //使用简单的好友管理器，假设所有在线用户都是好友。（仅仅用于demo）
                DefaultFriendsManager friendManager = new DefaultFriendsManager();
                RapidServerEngine.FriendsManager = friendManager;

                //使用简单的组管理器，假设所有在线用户都是一个组。（仅仅用于demo）
                DefaultGroupManager groupManager = new DefaultGroupManager();
                RapidServerEngine.GroupManager = groupManager;
                
                

                //初始化服务端引擎
                RapidServerEngine.Initialize(4530, new CustomizeHandler(), new BasicHandler());
                friendManager.UserManager = RapidServerEngine.UserManager; //RapidServerEngine初始化成功后，其UserManager属性才可用。
                groupManager.UserManager = RapidServerEngine.UserManager;
                
                //设置重登陆模式
                RapidServerEngine.UserManager.RelogonMode = RelogonMode.ReplaceOld;

                //如果不需要默认的UI显示，可以替换下面这句为自己的Form
                ESPlus.Widgets.MainServerForm mainForm = new ESPlus.Widgets.MainServerForm(RapidServerEngine);
                mainForm.CustomFunctionActivated += new ESBasic.CbGeneric(mainForm_CustomFunctionActivated);
                Application.Run(mainForm);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }


        static void mainForm_CustomFunctionActivated()
        {

            FunctionForm form = new FunctionForm(RapidServerEngine);
            form.Show();
        }
    }
}
