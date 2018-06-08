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
    public class WebwxSyncCheck
    {
        private static string GetUrl(string devid, LoginXmlResult loginInfo, SyncKeyData syncKey, long countid)
        {
            List<string> sklist = new List<string>();
            syncKey.List.ForEach(x =>
            {
                sklist.Add(x.Key + "_" + x.Val.ToString());
            });
            string synckeystr = HttpUtility.UrlEncode(string.Join("|", sklist));
            string url = "https://webpush.wx.qq.com/cgi-bin/mmwebwx-bin/synccheck?r=" + Tools.Timestamp().ToString() +
                "&skey=" + HttpUtility.UrlEncode(loginInfo.skey) +
                "&sid=" + loginInfo.wxsid +
                "&uin=" + loginInfo.wxuin +
                "&deviceid=" + devid +
                "&synckey=" + synckeystr +
                "&_=" + countid.ToString();

            return url;
        }
        public static void GetData(Http http, string devid, LoginXmlResult loginInfo, SyncKeyData syncKey,long countid,AjaxCallBack callback)
        {
            string url = GetUrl(devid,loginInfo, syncKey, countid);
            http.GetAsync(url, callback);
        }
        public static string GetData(Http http, string devid, LoginXmlResult loginInfo, SyncKeyData syncKey, long countid)
        {
            string url = GetUrl(devid, loginInfo, syncKey, countid);
            return http.Get(url);
        }
    }
}
