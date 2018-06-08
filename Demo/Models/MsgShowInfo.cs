using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class MsgShowInfo
    {
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string NickName { get; set; }
        
        public Image HeadImg { get; set; }
        public int MsgType { get; set; }
        public string Content { get; set; }
        public bool isMe { get; set; }

    }
}
