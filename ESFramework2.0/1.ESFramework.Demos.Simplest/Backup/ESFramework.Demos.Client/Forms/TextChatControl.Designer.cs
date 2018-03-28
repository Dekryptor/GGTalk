namespace ESFramework.Demos.Client
{
    partial class TextChatControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_send = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.agileRichTextBox_send = new ESBasic.Widget.AgileRichTextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.agileRichTextBox_history = new ESBasic.Widget.AgileRichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_send);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Controls.Add(this.agileRichTextBox_send);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 155);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 140);
            this.panel1.TabIndex = 6;
            // 
            // button_send
            // 
            this.button_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_send.Location = new System.Drawing.Point(375, 109);
            this.button_send.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(67, 28);
            this.button_send.TabIndex = 10;
            this.button_send.Text = "发送";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(445, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // agileRichTextBox_send
            // 
            this.agileRichTextBox_send.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.agileRichTextBox_send.Location = new System.Drawing.Point(4, 35);
            this.agileRichTextBox_send.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.agileRichTextBox_send.Name = "agileRichTextBox_send";
            this.agileRichTextBox_send.Size = new System.Drawing.Size(436, 65);
            this.agileRichTextBox_send.TabIndex = 1;
            this.agileRichTextBox_send.Text = "";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 151);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(445, 4);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // agileRichTextBox_history
            // 
            this.agileRichTextBox_history.BackColor = System.Drawing.Color.White;
            this.agileRichTextBox_history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.agileRichTextBox_history.Location = new System.Drawing.Point(0, 0);
            this.agileRichTextBox_history.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.agileRichTextBox_history.Name = "agileRichTextBox_history";
            this.agileRichTextBox_history.ReadOnly = true;
            this.agileRichTextBox_history.Size = new System.Drawing.Size(445, 151);
            this.agileRichTextBox_history.TabIndex = 8;
            this.agileRichTextBox_history.Text = "";
            // 
            // TextChatControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.agileRichTextBox_history);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "TextChatControl";
            this.Size = new System.Drawing.Size(445, 295);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_send;
        private ESBasic.Widget.AgileRichTextBox agileRichTextBox_send;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Splitter splitter1;
        private ESBasic.Widget.AgileRichTextBox agileRichTextBox_history;
    }
}
