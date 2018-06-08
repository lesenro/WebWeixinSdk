using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebWeixin.Utils;

namespace WebWeixin.Api
{
    public class LoginUuid
    {
        private static string GetUrl(long timeStamp)
        {
            string appid = "wx782c26e4c19acffb";
            string url = "https://login.wx.qq.com/jslogin?appid={appid}&redirect_uri=https%3A%2F%2Fwx.qq.com%2Fcgi-bin%2Fmmwebwx-bin%2Fwebwxnewloginpage&fun=new&lang=zh_CN&_=";
            return url.Replace("{appid}", appid) + timeStamp.ToString();
        }
        private static string GetUuid(string str)
        {
            Regex reg = new Regex("window.QRLogin.uuid = \"(?<uuid>.*?)\"");
            Match match = reg.Match(str);
            return match.Groups["uuid"].Value;
        }
        public static string GetUuid(long timeStamp, Http http)
        {
            string result = http.Get(GetUrl(timeStamp));
            return GetUuid(result);
           // var report = BaseRequest.WebwxStatReport(DeviceID, Tools.Timestamp());
           // httpJson.PostAsync(ApiAddress.Webwxstatreport(), JsonConvert.SerializeObject(report));
        }
    }
}
