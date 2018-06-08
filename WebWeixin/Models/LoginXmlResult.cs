using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebWeixin.Models
{
    [XmlRoot("error")]
    public class LoginXmlResult
    {
        public int reg { get; set; }
        public string message { get; set; }
        public string skey { get; set; }
        public string wxsid { get; set; }
        public string wxuin { get; set; }
        public string pass_ticket { get; set; }
        public int isgrayscale { get; set; }
    }
}
