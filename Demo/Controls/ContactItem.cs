using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebWeixin.Models;
using Demo.Models;

namespace Demo.Controls
{
    public partial class ContactItem : UserControl
    {
        public Image Avatar {
            get
            {
                return picAvatar.Image;
            }
            set
            {
                this.picAvatar.Image = value;
            }
        }
        public string NickName
        {
            get
            {
                return ContactInfo.NickName;
            }
        }
        public List<MsgShowInfo> msgList { get; set; } = new List<MsgShowInfo>();

        public Contact ContactInfo { get; set; }
        public List<Contact> ContactList { get; set; }
        public bool HeadImgUpdateFlag { get; set; } = false;
        public ContactItem()
        {
            InitializeComponent();
        }
        public ContactItem(Contact contact)
        {
            InitializeComponent();
            ContactInfo = contact;
            labName.Text = string.IsNullOrEmpty( contact.RemarkName)? contact.NickName: contact.RemarkName;
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONUP = 0x0021;
            IntPtr param = (IntPtr)33619969;
            
           
            if (m.Msg == WM_NCLBUTTONUP&&m.LParam== param)
            {
                this.OnClick(new EventArgs());
                base.WndProc(ref m);
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
