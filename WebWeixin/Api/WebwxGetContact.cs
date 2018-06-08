using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebWeixin.Models;
using WebWeixin.Utils;

namespace WebWeixin.Api
{
    public class WebwxGetContact
    {
        private static string GetUrl(LoginXmlResult loginInfo)
        {
            return "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxgetcontact?pass_ticket=" + HttpUtility.UrlEncode(loginInfo.pass_ticket)  + 
                "&r="+ Tools.Timestamp().ToString() + "&seq=0&skey=" + loginInfo.skey;
        }

        public static void GetData(Http http, LoginXmlResult loginInfo, AjaxCallBack callback)
        {
            string url = GetUrl(loginInfo);
            http.GetAsync(url, callback);
        }
        public static string GetData(Http http, LoginXmlResult loginInfo)
        {
            string url = GetUrl(loginInfo);
            return http.Encoder.GetString(http.GetData(url));
        }
    }
}
