using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.Models;

namespace Demo.Controls
{
    public partial class MsgItem : UserControl
    {
        MsgShowInfo msgInfo { get; set; }
        public MsgItem(MsgShowInfo minfo)
        {
            InitializeComponent();
            msgInfo = minfo;
        }

        private void MsgItem_Load(object sender, EventArgs e)
        {
            labMsg.Text = msgInfo.Content;
            labMsg.AutoSize = true;
            picAvatar.Image = msgInfo.HeadImg;
            if (msgInfo.isMe)
            {
                panContent.Padding = new Padding(10);
                panAvatar.Dock = DockStyle.Right;
                panContent.Dock = DockStyle.Right;
                panContent.BackColor = Color.Green;
                labMsg.ForeColor = Color.White;
            }
            else
            {
                panContent.Padding = new Padding(10);

                panAvatar.Dock = DockStyle.Left;
                panContent.Dock = DockStyle.Left;
                panContent.BackColor = Color.White;
            }
            panAvatar.SendToBack();

        }
        private string MsgFormat(string msg)
        {
            int maxw = Width - 250;
            maxw = maxw < 250 ? 250 : maxw;
            if (labMsg.Width < maxw) {
                return msg;
            }
            Graphics g = CreateGraphics();

            bool newLine = true;
            StringBuilder sb = new StringBuilder();
            StringBuilder line = new StringBuilder();
            foreach(var b in msg)
            {
                int ib = (int)b;
                if (ib > 127 && newLine)
                {
                    newLine = false;
                }else if (ib > 127 && !newLine)
                {
                    newLine = true;
                }
                if (ib < 127)
                {
                    newLine = true;
                }
                line.Append(b);
                if (newLine) {
                    SizeF sf = g.MeasureString(line.ToString(), labMsg.Font);
                    if (sf.Width > maxw)
                    {
                        sb.Append(line.ToString() + "\r\n");
                        line.Clear();
                    }
                }

            }
            return sb.ToString();
        }
        public void UpdateShow()
        {
            labMsg.Text = MsgFormat(labMsg.Text);
            labMsg.Update();
            panContent.Width = labMsg.Width + panContent.Padding.Left + panContent.Padding.Right;
            int msgh = labMsg.Height + panContent.Padding.Top + panContent.Padding.Bottom + Padding.Top + Padding.Bottom;
            int headh = Padding.Top + Padding.Bottom + picAvatar.Height;
            Height = msgh > headh ? msgh : headh;
            labMsg.AutoSize = false;
            labMsg.Dock = DockStyle.Fill;
            if (Width > 1)
            {
                return;
            }
        }
        
    }
}
