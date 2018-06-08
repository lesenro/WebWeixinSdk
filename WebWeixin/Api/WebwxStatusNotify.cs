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
    public class WebwxStatusNotify
    {
        private static string GetUrl(string pass_ticket)
        {
            return "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxstatusnotify?lang=zh_CN&pass_ticket="+ HttpUtility.UrlEncode(pass_ticket);
        }
        private static string GetParams(string devid, LoginXmlResult loginInfo, int code,string fromUser,string toUser)
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
                ClientMsgId = Tools.Timestamp(),
                Code = code,
                FromUserName= fromUser,
                ToUserName = toUser,
            };
            return JsonConvert.SerializeObject(param);
        }

        public static void GetData(Http http, string devid, LoginXmlResult loginInfo, int code, string fromUser, string toUser, AjaxCallBack callback)
        {
            string url = GetUrl(loginInfo.pass_ticket);
            string param = GetParams(devid, loginInfo, code, fromUser, toUser);
            http.PostDataAsync(url, param, callback);
        }
        public static string GetData(Http http, string devid, LoginXmlResult loginInfo, int code, string fromUser, string toUser)
        {
            string url = GetUrl(loginInfo.pass_ticket);
            string param = GetParams(devid, loginInfo, code, fromUser, toUser);
            var bytes = http.PostData(url, param);
            return http.Encoder.GetString(bytes);
        }
    }
}
