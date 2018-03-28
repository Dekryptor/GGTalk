namespace ESFramework.Demos.Client
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.panel_plus = new System.Windows.Forms.Panel();
            this.fileTransferingViewer1 = new ESPlus.Widgets.FileTransferingViewer();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textChatControl1 = new ESFramework.Demos.Client.TextChatControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel_plus.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_plus
            // 
            this.panel_plus.Controls.Add(this.fileTransferingViewer1);
            this.panel_plus.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_plus.Location = new System.Drawing.Point(645, 0);
            this.panel_plus.Margin = new System.Windows.Forms.Padding(4);
            this.panel_plus.Name = "panel_plus";
            this.panel_plus.Size = new System.Drawing.Size(267, 470);
            this.panel_plus.TabIndex = 1;
            // 
            // fileTransferingViewer1
            // 
            this.fileTransferingViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTransferingViewer1.Location = new System.Drawing.Point(0, 0);
            this.fileTransferingViewer1.Margin = new System.Windows.Forms.Padding(5);
            this.fileTransferingViewer1.Name = "fileTransferingViewer1";
            this.fileTransferingViewer1.Size = new System.Drawing.Size(267, 470);
            this.fileTransferingViewer1.TabIndex = 0;
            this.fileTransferingViewer1.FileResumedTransStarted += new ESBasic.CbGeneric<string, bool>(this.fileTransferingViewer1_FileResumedTransStarted);
            this.fileTransferingViewer1.FileTransCompleted += new ESBasic.CbGeneric<string, bool>(this.fileTransferingViewer1_FileTransCompleted);
            this.fileTransferingViewer1.FileTransDisruptted += new ESBasic.CbGeneric<string, bool, ESPlus.FileTransceiver.FileTransDisrupttedType>(this.fileTransferingViewer1_FileTransDisruptted);
            this.fileTransferingViewer1.FileTransStarted += new ESBasic.CbGeneric<string, bool>(this.fileTransferingViewer1_FileTransStarted);
            this.fileTransferingViewer1.AllTaskFinished += new ESBasic.CbSimple(this.fileTransferingViewer1_AllTaskFinished);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(641, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 470);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textChatControl1);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(641, 470);
            this.panel2.TabIndex = 3;
            // 
            // textChatControl1
            // 
            this.textChatControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textChatControl1.Location = new System.Drawing.Point(0, 38);
            this.textChatControl1.Margin = new System.Windows.Forms.Padding(5);
            this.textChatControl1.Name = "textChatControl1";
            this.textChatControl1.Size = new System.Drawing.Size(641, 432);
            this.textChatControl1.TabIndex = 6;
            this.textChatControl1.Load += new System.EventHandler(this.textChatControl1_Load_1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(641, 38);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "文件传输";
            this.toolStrip1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(32, 35);
            this.toolStripButton1.Text = "传输文件";
            // 
            // ChatForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 470);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel_plus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChatForm";
            this.Text = "正在与aa01对话中...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.panel_plus.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_plus;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private ESPlus.Widgets.FileTransferingViewer fileTransferingViewer1;       
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private TextChatControl textChatControl1;

    }
}