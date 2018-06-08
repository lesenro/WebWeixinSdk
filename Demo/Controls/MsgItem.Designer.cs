namespace Demo.Controls
{
    partial class MsgItem
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.panContent = new System.Windows.Forms.Panel();
            this.panAvatar = new System.Windows.Forms.Panel();
            this.labMsg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.panContent.SuspendLayout();
            this.panAvatar.SuspendLayout();
            this.SuspendLayout();
            // 
            // picAvatar
            // 
            this.picAvatar.Dock = System.Windows.Forms.DockStyle.Top;
            this.picAvatar.Location = new System.Drawing.Point(15, 0);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(40, 40);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            // 
            // panContent
            // 
            this.panContent.Controls.Add(this.labMsg);
            this.panContent.Location = new System.Drawing.Point(179, 30);
            this.panContent.Name = "panContent";
            this.panContent.Size = new System.Drawing.Size(146, 100);
            this.panContent.TabIndex = 1;
            // 
            // panAvatar
            // 
            this.panAvatar.Controls.Add(this.picAvatar);
            this.panAvatar.Location = new System.Drawing.Point(17, 27);
            this.panAvatar.Name = "panAvatar";
            this.panAvatar.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.panAvatar.Size = new System.Drawing.Size(70, 78);
            this.panAvatar.TabIndex = 2;
            // 
            // labMsg
            // 
            this.labMsg.AutoSize = true;
            this.labMsg.Location = new System.Drawing.Point(42, 43);
            this.labMsg.Name = "labMsg";
            this.labMsg.Size = new System.Drawing.Size(41, 12);
            this.labMsg.TabIndex = 0;
            this.labMsg.Text = "label1";
            // 
            // MsgItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panAvatar);
            this.Controls.Add(this.panContent);
            this.Name = "MsgItem";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(530, 133);
            this.Load += new System.EventHandler(this.MsgItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.panContent.ResumeLayout(false);
            this.panContent.PerformLayout();
            this.panAvatar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Panel panContent;
        private System.Windows.Forms.Panel panAvatar;
        private System.Windows.Forms.Label labMsg;
    }
}
