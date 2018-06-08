using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class SendMsgInfo
    {
        public string ClientMsgId { get; set; }
        public string Content { get; set; }
        public string FromUserName { get; set; }
        public string LocalID { get; set; }
        public string ToUserName { get; set; }
        public int Type { get; set; }
    }
}
