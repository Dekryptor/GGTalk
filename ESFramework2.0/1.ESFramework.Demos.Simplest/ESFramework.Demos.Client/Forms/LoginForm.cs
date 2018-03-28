using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESPlus.Application.Basic.Passive;
using ESPlus.Rapid;
using ESPlus.Application.Basic;
using ESBasic;

namespace ESFramework.Demos.Client
{
    public partial class LoginForm : Form
    {
        private IRapidPassiveEngine rapidPassiveEngine;
        private MainForm mainForm;

        public LoginForm( IRapidPassiveEngine engine ,MainForm _mainForm)
        {
            InitializeComponent();
            this.rapidPassiveEngine = engine;
            this.mainForm = _mainForm;
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string userID = this.textBox_id.Text.Trim();
            if (userID.Length > 10)
            {                
                MessageBox.Show("ID长度必须小于10.");
                return;
            }                     

            LogonResult logonResult = LogonResult.Succeed;//????????????????????????????????????????????????
            try
            {
                this.rapidPassiveEngine.SystemToken = "" ; //系统标志
                //初始化引擎并登录，返回登录结果。如果登陆成功，引擎将与当前用户绑定。
                logonResult = this.rapidPassiveEngine.Initialize(userID, this.textBox_pwd.Text, "127.0.0.1", 4530, this.mainForm).LogonResult;//59.175.145.163
            }
            catch (Exception ee)
            {
                MessageBox.Show(string.Format("连接服务器失败。{0}" ,ee.Message));
                return;
            }

            if (logonResult == LogonResult.Failed)
            {
                MessageBox.Show("用户名或密码错误！");
                return;
            }

            if (logonResult == LogonResult.HadLoggedOn)
            {
                MessageBox.Show("已经在其它地方登陆！");
                return;
            }


            this.DialogResult = DialogResult.OK;//???????????????????????????????????????????????????????????         
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }     
       
    }
}
