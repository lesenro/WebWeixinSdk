using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class MsgInfo
    {
        public string MsgId { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public int MsgType { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int ImgStatus { get; set; }
        public long CreateTime { get; set; }
        public long VoiceLength { get; set; }
        public long PlayLength { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string MediaId { get; set; }
        public string Url { get; set; }
        public int AppMsgType { get; set; }
        public int StatusNotifyCode { get; set; }
        public string StatusNotifyUserName { get; set; }
        public MsgRecommendInfo RecommendInfo { get; set; }
        public int ForwardFlag { get; set; }
        public MsgAppInfo AppInfo { get; set; }
        public int HasProductId { get; set; }
        public string Ticket { get; set; }
        public int ImgHeight { get; set; }
        public int ImgWidth { get; set; }
        public int SubMsgType { get; set; }
        public long NewMsgId { get; set; }
        public string OriContent { get; set; }
        public string EncryFileName { get; set; }

    }
}
