using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class WebwxContactInfo
    {
        public BaseResponse BaseResponse { get; set; }
        public int MemberCount { get; set; }
        public List<Contact> MemberList { get; set; }
        public int Seq { get; set; }
    }
}
