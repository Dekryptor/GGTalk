using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ESBasic.ObjectManagement.Forms;
using ESPlus.Application;
using ESPlus.Application.CustomizeInfo.Passive;
using ESPlus.Application.FileTransfering.Passive;
using ESPlus.FileTransceiver;
using ESBasic;
using ESFramework.Demos.Core;

namespace ESFramework.Demos.Client
{
    public partial class ChatForm : Form, IManagedForm<string>
    {
        private string friendID;
        private string currentUserID;
        private IFileOutter fileOutter;
        private ICustomizeOutter customizeInfoOutter;

        #region Ctor
        public ChatForm(string _currentUserID, string _friendID, ICustomizeOutter _customizeInfoOutter, IFileOutter _fileOutter)
        {
            InitializeComponent();
            this.currentUserID = _currentUserID;
            this.friendID = _friendID;
            this.fileOutter = _fileOutter;

            this.fileTransferingViewer1.Initialize(this.friendID, this.fileOutter);
            this.customizeInfoOutter = _customizeInfoOutter;
            this.textChatControl1.Initialize(_currentUserID, _friendID, _customizeInfoOutter);
            this.panel_plus.Visible = false;
            this.Width = 500;

            this.Text = string.Format("正在与{0}对话中...", _friendID);
        } 
        #endregion           

        #region IManagedForm<string> 成员

        public string FormID
        {
            get { return this.friendID; }
        }

        #endregion

        #region ShowOtherTextChat
        /// <summary>
        /// 显示好友发送过来的消息
        /// </summary>        
        public void ShowOtherTextChat(string userID, string text)
        {
            this.textChatControl1.ShowOtherTextChat(userID, text);
        } 
        #endregion

        #region SelfOffline，FriendOffline
        /// <summary>
        /// 自己掉线
        /// </summary>
        public void SelfOffline()
        {
            this.textChatControl1.ButtonSend.Enabled = false;
            this.Text += "     自己已经掉线";
            this.toolStripButton1.Enabled = false;
        }

        /// <summary>
        /// 好友掉线
        /// </summary>
        public void FriendOffline()
        {
            this.textChatControl1.ButtonSend.Enabled = false;
            this.Text += "     好友已经掉线";
            this.toolStripButton1.Enabled = false;
        } 
        #endregion       

        #region OnFileRequest
        //当接收到对方的文件传送请求时
        public void OnFileRequest(string fileID, string senderID, string fileName)
        {
            if (DialogResult.OK == MessageBox.Show(string.Format("{0}要求向你传输文件{1}，你是否同意接收？", senderID, fileName), "文件传输", MessageBoxButtons.OKCancel))
            {
                string savePath = ESBasic.Helpers.FileHelper.GetPathToSave("保存", fileName, null);
                if (!string.IsNullOrEmpty(savePath))
                {
                    this.fileOutter.BeginReceiveFile(fileID, savePath);
                }
                else
                {
                    this.RejectFile(fileID, fileName);
                }
            }
            else
            {
                this.RejectFile(fileID, fileName);
            }
        }

        private void RejectFile(string fileID, string fileName)
        {
            TransferingProject fileInfo = this.fileOutter.GetTransferingProject(fileID);
            if (fileInfo != null)
            {
                this.textChatControl1.ShowRejectFile(fileName);
                this.fileOutter.RejectFile(fileID);
            }
        } 
        #endregion

        #region OnFileResponse
        //对方同意或拒绝接收文件
        public void OnFileResponse(TransferingProject transferingProject, bool agreeReceive)
        {
            this.textChatControl1.ShowSystemMessage(transferingProject, agreeReceive);
        }  
        #endregion

        #region 请求发送文件
        //请求发送文件       
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string filePath = ESBasic.Helpers.FileHelper.GetFileToOpen("打开");
            if (filePath == null)
            {
                return;
            }
            string fileID;
            SendingFileParas sendingFileParas = new SendingFileParas(20480, 0);//文件数据包大小，可以根据网络状况设定，局网内可以设为204800，传输速度可以达到30M/s以上；公网建议设定为2048或4096或8192
            this.fileOutter.BeginSendFile(this.friendID, filePath, null, sendingFileParas, out fileID);
        } 
        #endregion        

        #region 关闭窗口前，判断当前是否有文件正在传输
        //关闭窗口前，判断当前是否有文件正在传输，如果有，则提示用户是否
        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> fileIDs = this.fileOutter.GetTransferingAbout(this.friendID);
            if (fileIDs != null && fileIDs.Count > 0)
            {
                DialogResult result = MessageBox.Show("如果关闭窗口，就会中止文件传送。是否关闭窗口？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None);
                if (result == DialogResult.OK)
                {
                    this.fileOutter.CancelTransferingAbout(this.friendID);
                }
                else
                {
                    e.Cancel = true;
                }
            }

        } 
        #endregion

        #region 文件传送事件

        //文件传送开始时，设置进度面板可见性
        private void fileTransferingViewer1_FileTransStarted(string obj1, bool obj2)
        {
            if (!this.panel_plus.Visible)
            {
                this.panel_plus.Visible = true;
                this.Width = 700;
            }
        }

        /// <summary>
        /// 文件传输中断
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="isSender">是接收者，还是发送者</param>
        /// <param name="fileTransDisrupttedType">中断原因</param>
        private void fileTransferingViewer1_FileTransDisruptted(string fileName, bool isSender, FileTransDisrupttedType fileTransDisrupttedType)
        {
            this.textChatControl1.ShowFileTransferFailed(fileName, isSender, fileTransDisrupttedType);
            if (!this.fileTransferingViewer1.IsFileTransfering)
            {
                this.panel_plus.Visible = false;
                this.Width = 500;
            }
        }

        /// <summary>
        /// 文件续传开始
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="isSender">接收者，还是发送者</param>
        private void fileTransferingViewer1_FileResumedTransStarted(string fileName, bool isSender)
        {
            this.textChatControl1.ShowFileResumedTransStarted(fileName);
        }

        /// <summary>
        /// 文件传输成功
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="isSender">接收者，还是发送者</param>
        private void fileTransferingViewer1_FileTransCompleted(string fileName, bool isSender)
        {
            this.textChatControl1.ShowFileTransCompleted(fileName, isSender);
        }

        //所有文件传送完成
        private void fileTransferingViewer1_AllTaskFinished()
        {
            if (this.panel_plus.Visible)
            {
                this.panel_plus.Visible = false;
                this.Width = 500;
            }
        } 
        #endregion             

        internal void ShowSenderCancelMessage(string fileName)
        {
            this.textChatControl1.ShowSenderCancelMessage(fileName);
        }

        private void textChatControl1_Load(object sender, EventArgs e)
        {

        }

        private void textChatControl1_Load_1(object sender, EventArgs e)
        {

        }

        

       
    }
}
