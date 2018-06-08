namespace Demo
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_login = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panUserList = new System.Windows.Forms.Panel();
            this.panLogin = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panMain = new System.Windows.Forms.Panel();
            this.panShow = new System.Windows.Forms.Panel();
            this.panMsgList = new System.Windows.Forms.Panel();
            this.msgListShow1 = new Demo.Controls.MsgListShow();
            this.panMsg = new System.Windows.Forms.Panel();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panSend = new System.Windows.Forms.Panel();
            this.btn_send = new System.Windows.Forms.Button();
            this.labTitle = new System.Windows.Forms.Label();
            this.autoReply = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panLogin.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panMain.SuspendLayout();
            this.panShow.SuspendLayout();
            this.panMsgList.SuspendLayout();
            this.panMsg.SuspendLayout();
            this.panSend.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_login
            // 
            this.btn_login.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_login.Location = new System.Drawing.Point(15, 185);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(170, 23);
            this.btn_login.TabIndex = 0;
            this.btn_login.Text = "点击登录";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(170, 155);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panUserList
            // 
            this.panUserList.AutoScroll = true;
            this.panUserList.Dock = System.Windows.Forms.DockStyle.Left;
            this.panUserList.Location = new System.Drawing.Point(0, 0);
            this.panUserList.Name = "panUserList";
            this.panUserList.Size = new System.Drawing.Size(250, 514);
            this.panUserList.TabIndex = 7;
            // 
            // panLogin
            // 
            this.panLogin.Controls.Add(this.panel1);
            this.panLogin.Controls.Add(this.btn_login);
            this.panLogin.Location = new System.Drawing.Point(2, 2);
            this.panLogin.Name = "panLogin";
            this.panLogin.Padding = new System.Windows.Forms.Padding(15);
            this.panLogin.Size = new System.Drawing.Size(200, 223);
            this.panLogin.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 15);
            this.panel1.Size = new System.Drawing.Size(170, 170);
            this.panel1.TabIndex = 2;
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.panShow);
            this.panMain.Controls.Add(this.panUserList);
            this.panMain.Location = new System.Drawing.Point(246, 2);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(705, 514);
            this.panMain.TabIndex = 9;
            // 
            // panShow
            // 
            this.panShow.Controls.Add(this.panMsgList);
            this.panShow.Controls.Add(this.panMsg);
            this.panShow.Controls.Add(this.panSend);
            this.panShow.Controls.Add(this.labTitle);
            this.panShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panShow.Location = new System.Drawing.Point(250, 0);
            this.panShow.Name = "panShow";
            this.panShow.Padding = new System.Windows.Forms.Padding(5);
            this.panShow.Size = new System.Drawing.Size(455, 514);
            this.panShow.TabIndex = 8;
            // 
            // panMsgList
            // 
            this.panMsgList.Controls.Add(this.msgListShow1);
            this.panMsgList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMsgList.Location = new System.Drawing.Point(5, 39);
            this.panMsgList.Name = "panMsgList";
            this.panMsgList.Size = new System.Drawing.Size(445, 303);
            this.panMsgList.TabIndex = 3;
            // 
            // msgListShow1
            // 
            this.msgListShow1.AutoScroll = true;
            this.msgListShow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgListShow1.Location = new System.Drawing.Point(0, 0);
            this.msgListShow1.msgList = null;
            this.msgListShow1.Name = "msgListShow1";
            this.msgListShow1.Size = new System.Drawing.Size(445, 303);
            this.msgListShow1.TabIndex = 0;
            // 
            // panMsg
            // 
            this.panMsg.BackColor = System.Drawing.Color.White;
            this.panMsg.Controls.Add(this.txtMsg);
            this.panMsg.Controls.Add(this.toolStrip1);
            this.panMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panMsg.Location = new System.Drawing.Point(5, 342);
            this.panMsg.Name = "panMsg";
            this.panMsg.Padding = new System.Windows.Forms.Padding(5);
            this.panMsg.Size = new System.Drawing.Size(445, 123);
            this.panMsg.TabIndex = 2;
            // 
            // txtMsg
            // 
            this.txtMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Location = new System.Drawing.Point(5, 5);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(435, 113);
            this.txtMsg.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(5, 5);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(435, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // panSend
            // 
            this.panSend.Controls.Add(this.autoReply);
            this.panSend.Controls.Add(this.btn_send);
            this.panSend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panSend.Location = new System.Drawing.Point(5, 465);
            this.panSend.Name = "panSend";
            this.panSend.Size = new System.Drawing.Size(445, 44);
            this.panSend.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.Location = new System.Drawing.Point(362, 11);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 0;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // labTitle
            // 
            this.labTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.labTitle.Location = new System.Drawing.Point(5, 5);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(445, 34);
            this.labTitle.TabIndex = 0;
            this.labTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // autoReply
            // 
            this.autoReply.AutoSize = true;
            this.autoReply.Location = new System.Drawing.Point(5, 15);
            this.autoReply.Name = "autoReply";
            this.autoReply.Size = new System.Drawing.Size(72, 16);
            this.autoReply.TabIndex = 1;
            this.autoReply.Text = "自动回复";
            this.autoReply.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 567);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panLogin);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panLogin.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panMain.ResumeLayout(false);
            this.panShow.ResumeLayout(false);
            this.panMsgList.ResumeLayout(false);
            this.panMsg.ResumeLayout(false);
            this.panMsg.PerformLayout();
            this.panSend.ResumeLayout(false);
            this.panSend.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panUserList;
        private System.Windows.Forms.Panel panLogin;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panShow;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.Panel panMsg;
        private System.Windows.Forms.Panel panSend;
        private System.Windows.Forms.Panel panMsgList;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.MsgListShow msgListShow1;
        private System.Windows.Forms.CheckBox autoReply;
    }
}

