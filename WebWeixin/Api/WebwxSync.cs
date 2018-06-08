using Newtonsoft.Json;
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
    public class WebwxSync
    {
        //
        private static string GetUrl(LoginXmlResult loginInfo)
        {
            return "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsync?sid=" + loginInfo.wxsid +
                "&skey=" + loginInfo.skey + "&lang=zh_CN" +
                "&pass_ticket="+ HttpUtility.UrlEncode(loginInfo.pass_ticket);
        }
        private static string GetParams(string devid, LoginXmlResult loginInfo, SyncKeyData syncKey, long rnd)
        {

            dynamic param = new
            {
                BaseRequest = new
                {
                    DeviceID = devid,
                    Sid = loginInfo.wxsid,
                    Skey = loginInfo.skey,
                    Uin = loginInfo.wxuin
                },
                SyncKey = syncKey,
                rr = rnd,
            };
            return JsonConvert.SerializeObject(param);
        }
        public static void GetData(Http http, string devid, LoginXmlResult loginInfo, SyncKeyData syncKey, long rnd, AjaxCallBack callback)
        {
            string url = GetUrl(loginInfo);
            string param = GetParams(devid, loginInfo, syncKey, rnd);
            http.PostDataAsync(url, param, callback);
        }
    }

}
