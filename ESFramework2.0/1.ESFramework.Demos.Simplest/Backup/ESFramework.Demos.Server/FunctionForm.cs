using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ESFramework.Demos.Server
{
    public partial class FunctionForm : Form
    {
        string nobody = "无人上线";
        private ESPlus.Rapid.IRapidServerEngine rapidEngine;
        public FunctionForm(ESPlus.Rapid.IRapidServerEngine _rapidEngine)
        {
            InitializeComponent();
            this.rapidEngine = _rapidEngine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> list = this.rapidEngine.UserManager.GetOnlineUserList();
            if (list.Count > 0)
            {
                this.label1.Text = list[0];
            }
            else
            {
                this.label1.Text = this.nobody;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.label1.Text == this.nobody)
            {
                return;
            }
            this.rapidEngine.CustomizeController.Send(this.label1.Text, ESFramework.Demos.Core.InformationTypes.ServerSendToClient, System.Text.Encoding.UTF8.GetBytes("123"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.label1.Text == this.nobody)
            {
                return;
            }
            byte[] returunInfo = this.rapidEngine.CustomizeController.QueryLocalClient(this.label1.Text, ESFramework.Demos.Core.InformationTypes.ServerCallClient, null);
            if (returunInfo != null)
            {
                this.label2.Text = System.Text.Encoding.UTF8.GetString(returunInfo);
            }
            
        }
    }
}
