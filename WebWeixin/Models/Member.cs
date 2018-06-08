using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class Member
    {
        public long Uin { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public long AttrStatus { get; set; }
        public string PYInitial { get; set; }
        public string PYQuanPin { get; set; }
        public string RemarkPYInitial { get; set; }
        public string RemarkPYQuanPin { get; set; }
        public int MemberStatus { get; set; }
        public string DisplayName { get; set; }
        public string KeyWord { get; set; }
    }
}
