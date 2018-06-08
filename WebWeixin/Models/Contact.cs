using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class Contact
    {
        public long Uin { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string HeadImgUrl { get; set; }
        public int ContactFlag { get; set; }
        public int MemberCount { get; set; }
        public int HeadImgUpdateFlag { get; set; }
        public int ContactType { get; set; }
        public string ChatRoomOwner { get; set; }
        public List<Member> MemberList { get; set; }
        public string RemarkName { get; set; }
        public int HideInputBarFlag { get; set; }
        public int Sex { get; set; }
        public string Signature { get; set; }
        public int VerifyFlag { get; set; }
        public int OwnerUin { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public int StarFriend { get; set; }
        public int AppAccountFlag { get; set; }
        public int Statues { get; set; }
        public long AttrStatus { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Alias { get; set; }
        public int SnsFlag { get; set; }
        public int UniFriend { get; set; }
        public string DisplayName { get; set; }
        public int ChatRoomId { get; set; }
        public string KeyWord { get; set; }
        public string EncryChatRoomId { get; set; }
        public int IsOwner { get; set; }
    }
}
