using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESPlus.Core;
using ESPlus.Application.Basic.Passive;
using ESBasic.ObjectManagement;
using ESPlus.Rapid;

/*
 * 本demo采用的是ESFramework的免费版本，若想获取ESFramework其它版本，请联系 www.oraycn.com 或 QQ：168757008。
 * 
 */
namespace ESFramework.Demos.Client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                IRapidPassiveEngine rapidPassiveEngine = ESPlus.Rapid.RapidEngineFactory.CreatePassiveEngine();
                MainForm mainForm = new MainForm();
                LoginForm loginForm = new LoginForm(rapidPassiveEngine, mainForm); //在LoginForm中初始化客户端引擎RapidPassiveEngine                
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                mainForm.Initialize(rapidPassiveEngine);                
                Application.Run(mainForm);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
    }
}
