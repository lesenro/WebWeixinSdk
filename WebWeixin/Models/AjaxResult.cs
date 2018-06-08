using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public enum AjaxTypes
    {
        WxInit, WxContact, WxStatusNotify,WxSync, WxBatchGetContact
    }
    public class AjaxResult
    {
        public AjaxTypes AjaxType { get; set; }
        public dynamic Data { get; set; }
        public string DataKey { get; set; }
    }
}
