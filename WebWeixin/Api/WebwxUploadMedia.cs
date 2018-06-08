using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebWeixin.Models;
using WebWeixin.Utils;

namespace WebWeixin.Api
{
    public class WebwxUploadMedia
    {
        private static string GetUrl()
        {
            return "https://file.wx.qq.com/cgi-bin/mmwebwx-bin/webwxuploadmedia?f=json";
        }
        public static dynamic GetParams(string devid, long timeStamp)
        {
            dynamic req = new
            {
                DeviceID = devid,
                Sid = "",
                Uin = "",
                Count = 0,
                List = new List<dynamic>(),
            };
            
            return req;
        }
        public static void GetData(Http http, string path)
        {
            var fcnt = File.ReadAllBytes(path);
            int maxChunk = 526113;
            MemoryStream ms = new MemoryStream(fcnt);
            
            for (int offset =0;offset<fcnt.LongLength;offset+= maxChunk)
            {
                if(offset+ maxChunk < fcnt.LongLength)
                {
                    byte[] buffer = new byte[maxChunk];
                    ms.Read(buffer, offset, maxChunk);
                    http.PostDataAsync(GetUrl(), buffer);
                }
            }
            

        }
        public static void GetData(Http http, string devid, long timeStamp)
        {
            var report = GetParams(devid, timeStamp);
            http.PostAsync(GetUrl(), JsonConvert.SerializeObject(report));
        }
    }
}
