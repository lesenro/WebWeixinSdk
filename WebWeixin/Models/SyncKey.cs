using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class SyncKeyData
    {
        public int Count { get; set; }
        public List<KeyVal> List { get; set; }
    }
    public class KeyVal
    {
        public int Key { get; set; }
        public long Val { get; set; }
    }
}
