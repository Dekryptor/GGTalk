using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESBasic.ObjectManagement;
using ESBasic.Emit.DynamicBridge;
using ESPlus.Application.Basic.Passive;

using ESBasic.ObjectManagement.Forms;
using ESBasic;
using ESFramework.Engine.Tcp.Passive;
using ESPlus.Rapid;
using ESPlus.Application.CustomizeInfo.Passive;
using ESFramework.Engine.ContractStyle.Stream;
using ESFramework.Demos.Core;
using ESPlus.Application.Basic;
using ESPlus.Application.CustomizeInfo;
using ESPlus.FileTransceiver;
using ESPlus.Application.FileTransfering;

namespace ESFramework.Demos.Client
{
    public partial class MainForm : Form,  ICustomizeHandler
    {
        private string userID;       
        private IRapidPassiveEngine rapidPassiveEngine;
        private FormManager<string, ChatForm> chatFormManager = new FormManager<string, ChatForm>();           

        public MainForm()
        {
            InitializeComponent();            
        }

        #region Initialize
        public void Initialize(IRapidPassiveEngine engine)
        {
            this.userID = engine.CurrentUserID;
            this.rapidPassiveEngine = engine;            

            //预订断线事件
            this.rapidPassiveEngine.ConnectionInterrupted += new CbGeneric(rapidPassiveEngine_ConnectionInterrupted);            
            //预订重连开始事件
            this.rapidPassiveEngine.ConnectionRebuildStart += new CbGeneric(rapidPassiveEngine_ConnectionRebuildStart);
            
            //预订重连成功并重新登录事件
            this.rapidPassiveEngine.RelogonCompleted += new CbGeneric<LogonResponse>(rapidPassiveEngine_RelogonCompleted);
            //预订好友上线的事件
            this.rapidPassiveEngine.FriendsOutter.FriendConnected += new CbGeneric<string>(FriendOutter_FriendConnected);
            //预订好友下线的事件
            this.rapidPassiveEngine.FriendsOutter.FriendOffline += new CbGeneric<string>(FriendOutter_FriendOffline);
           
            //预订自己被踢出掉线的事件
            this.rapidPassiveEngine.BasicOutter.BeingKickedOut += new CbGeneric(BasicOutter_BeingKickedOut);
            //预订自己被挤掉线的事件
            this.rapidPassiveEngine.BasicOutter.BeingPushedOut += new CbGeneric(BasicOutter_BeingPushedOut);

            //预定收到了来自发送方发送文件（夹）的请求的事件
            this.rapidPassiveEngine.FileOutter.FileRequestReceived += new CbFileRequestReceived(fileOutter_FileRequestReceived);
            //预定接收方回复了同意/拒绝接收文件（夹）时的事件
            this.rapidPassiveEngine.FileOutter.FileResponseReceived += new CbGeneric<TransferingProject, bool>(fileOutter_FileResponseReceived);
            //预订接收到广播消息的处理事件
            this.rapidPassiveEngine.GroupOutter.BroadcastReceived += new CbGeneric<string, string, int, byte[]>(GroupOutter_BroadcastReceived);

            this.toolStripLabel_loginfo.Text = string.Format("当前登录：{0}", this.userID);
            this.toolStripLabel_state.Text = "连接状态：正常";

            this.InitializeFriends();
        }

        
        
        private void InitializeFriends()
        {
            List<string> list = this.rapidPassiveEngine.BasicOutter.GetAllOnlineUsers();
            foreach (string friendID in list)
            {
                if (friendID != this.userID && !this.ListViewContains(friendID))
                {
                    this.listView1.Items.Add(friendID, 0);
                }
            }
        }

        #region ListViewContains
        private bool ListViewContains(string userID)
        {
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                if (this.listView1.Items[i].Text == userID)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
        #endregion

        #region IBasicOutter的事件
        #region 被挤掉线
        void BasicOutter_BeingPushedOut()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric(this.BasicOutter_BeingPushedOut), null);
            }
            else
            {
                this.OnDisconnected();
                this.toolStripLabel_state.Text = "连接状态：断开。在别处登录！";
                this.toolStripLabel_state.ForeColor = Color.Red;


            }
        } 
        #endregion

        #region 被踢出
        void BasicOutter_BeingKickedOut()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric(this.BasicOutter_BeingKickedOut), null);
            }
            else
            {
                this.OnDisconnected();
                this.toolStripLabel_state.Text = "连接状态：断开！";
                this.toolStripLabel_state.ForeColor = Color.Red;
                MessageBox.Show("您已经被提出！");
            }
        } 
        #endregion
        #endregion

        #region FriendOutter事件
        #region 好友下线
        void FriendOutter_FriendOffline(string friendID)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<string>(this.FriendOutter_FriendOffline), friendID);
            }
            else
            {
                ListViewItem target = null;
                foreach (ListViewItem item in this.listView1.Items)
                {
                    if (item.Text == friendID)
                    {
                        target = item;
                        break;
                    }
                }

                if (target != null)
                {
                    this.listView1.Items.Remove(target);
                }

                ChatForm form = this.chatFormManager.GetForm(friendID);
                if (form != null)
                {
                    form.FriendOffline();
                }
            }
        }
        #endregion

        #region 好友上线
        void FriendOutter_FriendConnected(string friendID)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<string>(this.FriendOutter_FriendConnected), friendID);
            }
            else
            {
                if (!this.ListViewContains(friendID))
                {
                    this.listView1.Items.Add(friendID, 0);
                }
            }
        }
        #endregion 
        #endregion


        #region 处理广播消息
        void GroupOutter_BroadcastReceived(string broadcastID, string groupID, int broadcastType, byte[] broadcastContent)
        {
             if (broadcastType == InformationTypes.Broadcast)

            {
                string broadcastText = System.Text.Encoding.UTF8.GetString(broadcastContent);
                broadcastText += "   这是" + broadcastID + "发送过来的广播消息";
                
                MessageBox.Show(broadcastText);
            }
        }  
        #endregion

        #region 网络状态变化事件
        #region 断线 重连 重新登录完成事件
        void rapidPassiveEngine_RelogonCompleted(LogonResponse relogonResult)
        {
            //如果在 重新登录的时候，数据库的密码 修改了，则这里relogonResult会返回登录失败 
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<LogonResponse>(this.rapidPassiveEngine_RelogonCompleted), relogonResult);
            }
            else
            {
                if (relogonResult.LogonResult != LogonResult.Succeed)
                {
                    return;
                }
                this.toolStripLabel_state.Text = "连接状态：正常（重连成功）";
                this.toolStripLabel_state.ForeColor = Color.Black;

                this.InitializeFriends();
            }
        } 
        #endregion

        #region 重连开始事件
        void rapidPassiveEngine_ConnectionRebuildStart()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbSimple(this.rapidPassiveEngine_ConnectionRebuildStart), null);
            }
            else
            {
                this.toolStripLabel_state.Text = "连接状态：断开，重连中......";
                this.toolStripLabel_state.ForeColor = Color.Red;
            }
        } 
        #endregion

        #region 断线事件
        void rapidPassiveEngine_ConnectionInterrupted()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbSimple(this.rapidPassiveEngine_ConnectionInterrupted), null);
            }
            else
            {
                this.OnDisconnected();
                this.toolStripLabel_state.Text = "连接状态：断开";
                this.toolStripLabel_state.ForeColor = Color.Red;
            }
        }  
        #endregion

        private void OnDisconnected()
        {
            this.listView1.Clear();

            foreach (ChatForm form in this.chatFormManager.GetAllForms())
            {
                form.SelfOffline();
            }
        }
        #endregion

        #region 双击鼠标弹出聊天窗口
        void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }

            ListViewHitTestInfo info = this.listView1.HitTest(e.Location);
            if (info.Item != null)
            {            
                ChatForm form = this.chatFormManager.GetForm(info.Item.Text);
                if (form == null)
                {
                    form = new ChatForm(this.userID, info.Item.Text, this.rapidPassiveEngine.CustomizeOutter,this.rapidPassiveEngine.FileOutter);
                    this.chatFormManager.Add(form);
                    form.Show();
                }

                form.Focus();
            }
        } 
        #endregion        

        #region ICustomizeHandler 实现 -- 处理接收到的自定义信息     
        /// <summary>
        ///  处理消息,如果sourceUserID为null， 则表示是服务端发送过来的消息；如果sourceUserID不为null， 则表示是其他客户端发送过来的消息
        /// </summary>       
        public void HandleInformation(string sourceUserID, int informationType, byte[] info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<string,int, byte[]>(this.HandleInformation), sourceUserID,informationType, info);
            }
            else
            {
                if (sourceUserID != null)
                {
                    if (informationType == InformationTypes.Chat)
                    {
                        ChatForm form = this.chatFormManager.GetForm(sourceUserID);
                        if (form == null)
                        {
                            form = new ChatForm(this.userID, sourceUserID, this.rapidPassiveEngine.CustomizeOutter,this.rapidPassiveEngine.FileOutter);
                            this.chatFormManager.Add(form);
                            form.Show();
                        }

                        form.Focus();

                        form.ShowOtherTextChat(sourceUserID, System.Text.Encoding.UTF8.GetString(info));
                    }
                    if (informationType == InformationTypes.SendBlob)
                    {
                        string msg = System.Text.Encoding.UTF8.GetString(info);
                        this.ShowMessage(msg);
                    }
                }
                else
                {
                    if (informationType == InformationTypes.ServerSendToClient)
                    {
                        string msg = System.Text.Encoding.UTF8.GetString(info);
                        MessageBox.Show(string.Format("收到服务端的消息：{0}", msg));
                    }
                }
            }
        }

        public byte[] HandleQuery(string sourceUserID, int informationType, byte[] info)
        {
            if (sourceUserID != null)//客户端同步调用
            {
                if (informationType == InformationTypes.ClientCallClient)
                {
                    this.ShowMessage(string.Format("收到好友{0}的同步调用请求，立即回复",sourceUserID));
                    return System.Text.Encoding.UTF8.GetBytes(this.userID + "已经收到你的同步调用请求！");
                }
            }
            else//服务端同步调用
            {
                if (informationType == InformationTypes.ServerCallClient)
                {
                    this.ShowMessage("收到服务端的同步调用请求，立即回复");
                    return System.Text.Encoding.UTF8.GetBytes(this.userID + "已经收到来自服务端的同步调用请求！");
                }
            }
            return null;
        }

        private void ShowMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<string>(this.ShowMessage), msg);
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        #endregion

        #region 文件传送的事件
        //当收到对方文件发送的请求时 的处理
        void fileOutter_FileRequestReceived(string fileID, string senderID, string fileName, ulong totalSize, ResumedProjectItem resumedProjectItem, string comment)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<string, string, string, ulong, ResumedProjectItem, string>(this.fileOutter_FileRequestReceived), fileID, senderID, fileName, totalSize, resumedProjectItem, comment);
            }
            else
            {
                ChatForm form = this.chatFormManager.GetForm(senderID);
                if (form == null)
                {
                    form = new ChatForm(this.userID, senderID, this.rapidPassiveEngine.CustomizeOutter, this.rapidPassiveEngine.FileOutter);
                    this.chatFormManager.Add(form);
                    form.Show();
                }

                form.Focus();
                form.OnFileRequest(fileID, senderID, fileName);
            }
        }

        /// <summary>
        /// 发送方收到 接收方（同意或者拒绝 接收文件）的回应时 的  处理
        /// </summary>   
        void fileOutter_FileResponseReceived(TransferingProject transferingProject, bool agreeReceive)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<TransferingProject, bool>(this.fileOutter_FileResponseReceived), transferingProject, agreeReceive);
            }
            else
            {
                ChatForm form = this.chatFormManager.GetForm(transferingProject.DestUserID);
                if (form != null)
                {
                    form.Focus();
                    form.OnFileResponse(transferingProject, agreeReceive);
                }
            }
        }
        #endregion

        #region 演示消息同步调用
        /// <summary>
        /// 演示消息同步调用。提交请求消息直接返回应答消息。
        /// </summary>       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] requestInfo = System.Text.Encoding.UTF8.GetBytes(this.toolStripTextBox1.Text);
                byte[] responseInfo = this.rapidPassiveEngine.CustomizeOutter.Query(InformationTypes.ClientCallServer, requestInfo);

                string responseMessage = System.Text.Encoding.UTF8.GetString(responseInfo);
                MessageBox.Show(responseMessage);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
      //向好友列表中的第一个好友 发送同步调用请求
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] requestInfo = System.Text.Encoding.UTF8.GetBytes(this.toolStripTextBox1.Text);

                string friendID = this.GetFirstFriendID();
                if (friendID == null) return;
                byte[] responseInfo = this.rapidPassiveEngine.CustomizeOutter.Query(friendID, InformationTypes.ClientCallClient, requestInfo);

                string responseMessage = System.Text.Encoding.UTF8.GetString(responseInfo);
                MessageBox.Show(responseMessage);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }
        #endregion
        private string GetFirstFriendID()
        {
            List<string> list = this.rapidPassiveEngine.BasicOutter.GetAllOnlineUsers();
            if (list.Count <= 0) return null;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == this.userID)
                {
                    list.RemoveAt(i);
                    break;
                }
            }
            if (list.Count <= 0) return null;
            return list[0];
        }
        //向第一个好友sendblob
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            string friendID = this.GetFirstFriendID();
                if (friendID == null) return;
            string a = "";
            for (int i = 0; i <= 100; i++)
            {
                a += "my name is baby. ";
            }

            this.rapidPassiveEngine.CustomizeOutter.SendBlob(friendID,InformationTypes.SendBlob, System.Text.Encoding.UTF8.GetBytes(a), 60);

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string text = "这是一条广播消息！";
            this.rapidPassiveEngine.GroupOutter.Broadcast("#0", InformationTypes.Broadcast, System.Text.Encoding.UTF8.GetBytes(text), ActionTypeOnChannelIsBusy.Discard);
        }


    }
}
