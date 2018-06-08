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
    public partial class MsgListShow : UserControl
    {
        public List<MsgShowInfo> msgList { get; set; }
        public void FillList( List<MsgShowInfo> mlist)
        {
            Clear();
            foreach(var item in mlist)
            {
                Add(item);
            }
            Point newPoint = new Point(0, Height - AutoScrollPosition.Y);
            AutoScrollPosition = newPoint;
        }
        public void Clear()
        {
            this.Controls.Clear();
            msgList?.Clear();
            msgList = new List<MsgShowInfo>();
        }
        public void Add(MsgShowInfo msgInfo)
        {
            msgList.Add(msgInfo);
            MsgItem item = new MsgItem(msgInfo);
            item.Dock = DockStyle.Top;
            Controls.Add(item);
            item.BringToFront();
            item.UpdateShow();

        }
        public MsgListShow()
        {
            InitializeComponent();
        }

        
    }
}
