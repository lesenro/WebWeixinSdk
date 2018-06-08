using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class ProfileInfo
    {
        public int BitFlag { get; set; }
        public BuffData UserName { get; set; }
        public BuffData NickName { get; set; }
        public long BindUin { get; set; }
        public BuffData BindEmail { get; set; }
        public BuffData BindMobile { get; set; }
        public int Status { get; set; }
        public int Sex { get; set; }
        public long PersonalCard { get; set; }
        public string Alias { get; set; }
        public int HeadImgUpdateFlag { get; set; }
        public string HeadImgUrl { get; set; }
        public string Signature { get; set; }
    }
    public class BuffData
    {
        public string Buff { get; set; }
    }
}
