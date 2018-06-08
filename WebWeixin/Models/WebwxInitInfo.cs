using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class WebwxInitInfo
    {
        public BaseResponse BaseResponse { get; set; }
        public int Count { get; set; }
        public List<Contact> ContactList { get; set; }
        public SyncKeyData SyncKey { get;set;}
        public User User { get; set; }
        public string ChatSet { get; set; }
        public string SKey { get; set; }
        public long ClientVersion { get; set; }
        public long SystemTime { get; set; }
        public int GrayScale { get; set; }
        public int InviteStartCount { get; set; }
        public int MPSubscribeMsgCount { get; set; }
        public List<MPSubscribeMsg> MPSubscribeMsgList { get; set; }
        public int ClickReportInterval { get; set; }
    }
}
