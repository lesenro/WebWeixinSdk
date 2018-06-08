using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Models
{
    public class UploadInfo
    {
        public BaseResponse BaseResponse { get; set; }
        public string MediaId { get; set; }
        public int StartPos { get; set; }
        public int CDNThumbImgHeight { get; set; }
        public int CDNThumbImgWidth { get; set; }
        public string EncryFileName { get; set; }
    }
}
