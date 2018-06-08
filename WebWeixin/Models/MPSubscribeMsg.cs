using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class MPSubscribeMsg
    {
        public string UserName { get; set; }
        public int MPArticleCount { get; set; }
        public List<MPArticle> MPArticleList { get; set; }
        public long Time { get; set; }
        public string NickName { get; set; }

    }
}
