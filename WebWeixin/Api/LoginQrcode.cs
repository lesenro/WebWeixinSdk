using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebWeixin.Utils;

namespace WebWeixin.Api
{
    public class LoginQrcode
    {
        private static string GetUrl(string uuid)
        {
            return "https://login.weixin.qq.com/qrcode/" + uuid;
        }
        public static Image QrCode(Http http,string uuid)
        {
            var bytes = http.GetData(GetUrl(uuid));
            MemoryStream stream = new MemoryStream(bytes);

            //var report = BaseRequest.WebwxStatReport(DeviceID, 0L);
            //httpJson.PostAsync(ApiAddress.Webwxstatreport(), JsonConvert.SerializeObject(report));
            return Image.FromStream(stream);
        }
        public static string MonitorUrl(string uuid , long ts ,long rnd)
        {
            return "https://login.wx.qq.com/cgi-bin/mmwebwx-bin/login?loginicon=true&uuid=" + uuid + "&tip=0&r=" + rnd.ToString() + "&_=" + ts.ToString();
        }
        public static byte[] UserAvatar(string result)
        {
            Regex reg = new Regex("window.userAvatar = \'data:img/jpg;base64,(?<avatar>.*?)\'");
            Match match = reg.Match(result);
            string base64str = match.Groups["avatar"].Value;
            return Convert.FromBase64String(base64str);
        }
        public static string LoginRedirectUri(string result)
        {
            Regex reg = new Regex("window.redirect_uri=\"(?<url>.*?)\"");
            Match match = reg.Match(result);
            string url = match.Groups["url"].Value;
            return url;
        }
    }
}
