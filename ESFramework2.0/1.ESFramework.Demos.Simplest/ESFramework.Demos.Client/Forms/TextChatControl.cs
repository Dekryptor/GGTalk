using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ESBasic.Widget;
using ESBasic.Collections;
using ESFramework.Passive;
using ESBasic;
using ESPlus.Application.CustomizeInfo.Passive;
using ESFramework.Demos.Core;
using ESPlus.FileTransceiver;

namespace ESFramework.Demos.Client
{
    public partial class TextChatControl : UserControl
    {
        
        private string userID = "";
        private ICustomizeOutter customizeInfoOutter = null;
        private string destUserID = "";

        #region ButtonSend
        /// <summary>
        /// ButtonSend 用于设为AcceptButton
        /// </summary>
        public Button ButtonSend
        {
            get
            {
                return this.button_send;
            }
        } 
        #endregion

        public TextChatControl()
        {
            InitializeComponent();
            this.fontDialog1.ShowColor = true;
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Face\\";
            this.agileRichTextBox_history.Initialize(new DefaultImagePathGetter(path ,".gif"));
            this.agileRichTextBox_send.Initialize(new DefaultImagePathGetter(path, ".gif"));                  
        }

        public void Initialize(string _userID, string friendID, ICustomizeOutter _customizeInfoOutter)
        {
           
            this.userID = _userID;
            this.destUserID = friendID;
            this.customizeInfoOutter = _customizeInfoOutter;
        }      

        

        private void button_send_Click(object sender, EventArgs e)
        {
            
            string text = this.agileRichTextBox_send.Text;
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("消息不能为空");
                return;
            }
            this.agileRichTextBox_history.AppendRichText(string.Format("{0} {1}\n  ", this.userID, DateTime.Now), null, null, Color.DarkGreen);
            this.agileRichTextBox_history.AppendRichText(text, null, null, Color.Black);
            this.agileRichTextBox_history.AppendText("\n");
            this.agileRichTextBox_history.ScrollToCaret();
            this.agileRichTextBox_send.Clear();
           
            //发送消息给好友
            this.customizeInfoOutter.Send(this.destUserID, InformationTypes.Chat, System.Text.Encoding.UTF8.GetBytes(text));
        }     

        public void ShowOtherTextChat(string userID, string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new CbGeneric<string, string>(this.ShowOtherTextChat), userID, text);
            }
            else
            {
                if (userID != this.userID)
                {                   
                    
                    this.agileRichTextBox_history.AppendRichText(string.Format("{0} {1}\n  ", userID, DateTime.Now), null, null, Color.DarkGreen);

                    this.agileRichTextBox_history.AppendRichText(text,null,null, Color.Black); //new Font("宋体",9)                       
                   
                    
                    this.agileRichTextBox_history.AppendText("\n");
                    this.agileRichTextBox_history.ScrollToCaret();
                    this.agileRichTextBox_send.Clear();                   
                }
            }
        }

        internal void ShowSystemMessage(TransferingProject transferingProject, bool agreed)
        {

            this.agileRichTextBox_history.AppendRichText(string.Format("系统  {0}\n", DateTime.Now), null, null, Color.DarkGray);
            if (agreed)
            {
                this.agileRichTextBox_history.AppendRichText(string.Format("对方同意接收{0}({1})。\n", transferingProject.ProjectName, transferingProject.TotalSize), null, null, Color.DarkGray);
            }
            else
            {
                this.agileRichTextBox_history.AppendRichText(string.Format("对方拒绝接收{0}({1})，文件传送中断。\n", transferingProject.ProjectName, transferingProject.TotalSize), null, null, Color.DarkGray);
            }
            this.agileRichTextBox_history.ScrollToCaret();
        }

        internal void ShowFileTransferFailed(string fileName, bool isSender, FileTransDisrupttedType fileTransDisrupttedType)
        {
            this.agileRichTextBox_history.AppendRichText(string.Format("系统  {0}\n", DateTime.Now), null, null, Color.DarkGray);
            string showText = "";
            switch (fileTransDisrupttedType)
            {
                case FileTransDisrupttedType.ActiveCancel:
                    {
                        if (isSender)
                        {
                            showText += string.Format("您取消了{0}的发送,文件传输失败\n ", fileName);
                        }
                        else
                        {
                            showText += string.Format("您中止了{0}的接收,文件传输失败\n ", fileName);
                        }
                        break;
                    }
                case FileTransDisrupttedType.DestCancel:
                    {
                        if (isSender)
                        {
                            showText += string.Format("对方中止了{0}的接收，文件传输失败 \n", fileName);
                        }
                        else
                        {
                            showText += string.Format("对方取消了{0}的发送 ，文件传输失败\n", fileName);
                        }

                        break;
                    }
                case FileTransDisrupttedType.DestOffline:
                    {
                        showText += "对方掉线，文件传输失败\n";
                        break;
                    }
                case FileTransDisrupttedType.SelfOffline:
                    {
                        showText += "自己掉线，文件传输失败\n";
                        break;
                    }
                case FileTransDisrupttedType.RejectAccepting:
                    {
                        showText += string.Format("对方拒绝接收{0},文件发送失败\n ", fileName);
                        break;
                    }
                case FileTransDisrupttedType.InnerError:
                    {
                        showText += "自己系统内部错误,文件传输失败\n";
                        break;
                    }
                case FileTransDisrupttedType.DestInnerError:
                    {
                        showText += "对方系统内部错误,文件传输失败\n";
                        break;
                    }
                case FileTransDisrupttedType.ReliableP2PChannelClosed:
                    {
                        showText += "P2P通道关闭,文件传输失败\n";
                        break;
                    }
            }

            this.agileRichTextBox_history.AppendRichText(showText, null, null, Color.DarkGray);
            this.agileRichTextBox_history.ScrollToCaret();
        }

        internal void ShowFileResumedTransStarted(string fileName)
        {
            this.agileRichTextBox_history.AppendRichText(string.Format("系统  {0}\n", DateTime.Now), null, null, Color.DarkGray);
            string showText = "文件" + fileName + "正在续传！\n";
            this.agileRichTextBox_history.AppendRichText(showText, null, null, Color.DarkGray);
            this.agileRichTextBox_history.ScrollToCaret();
        }

        internal void ShowFileTransCompleted(string fileName, bool isSender)
        {
            this.agileRichTextBox_history.AppendRichText(string.Format("系统  {0}\n", DateTime.Now), null, null, Color.DarkGray);
            string showText = string.Format("{0}文件{1}成功\n", isSender ? "发送" : "接收", fileName);
            this.agileRichTextBox_history.AppendRichText(showText, null, null, Color.DarkGray);
            this.agileRichTextBox_history.ScrollToCaret();
        }

        internal void ShowRejectFile(string fileName)
        {
            this.agileRichTextBox_history.AppendRichText(string.Format("系统  {0}\n", DateTime.Now), null, null, Color.DarkGray);
            string showText = string.Format("您拒绝接收{0},文件传输失败\n", fileName);
            this.agileRichTextBox_history.AppendRichText(showText, null, null, Color.DarkGray);
            this.agileRichTextBox_history.ScrollToCaret();
        }
        /// <summary>
        /// 发送方取消文件传输时，接收方 显示的消息
        /// </summary>
        /// <param name="fileName"></param>
        internal void ShowSenderCancelMessage(string fileName)
        {
            this.agileRichTextBox_history.AppendRichText(string.Format("系统  {0}\n", DateTime.Now), null, null, Color.DarkGray);
            string showText = string.Format("对方取消了{0}的发送，文件传输失败\n", fileName);
            this.agileRichTextBox_history.AppendRichText(showText, null, null, Color.DarkGray);
        }

    }   
}
