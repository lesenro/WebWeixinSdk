using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class WebwxSyncInfo
    {
        public BaseResponse BaseResponse { get; set; }
        public int AddMsgCount { get; set; }
        public List<MsgInfo> AddMsgList { get; set; }
        public int ModContactCount { get; set; }
        public List<Contact> ModContactList { get; set; }
        public int DelContactCount { get; set; }
        public List<Contact> DelContactList { get; set; }
        public int ModChatRoomMemberCount { get; set; }
        public List<string> ModChatRoomMemberList { get; set; }
        public ProfileInfo Profile { get; set; }
        public int ContinueFlag { get; set; }
        public SyncKeyData SyncKey { get; set; }
        public string SKey { get; set; }
        public SyncKeyData SyncCheckKey { get; set; }

    }
}
