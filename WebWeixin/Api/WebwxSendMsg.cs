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
    public class WebwxSendMsg
    {
        private static string GetUrl(string pass_ticket)
        {
            return "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxsendmsg?lang=zh_CN&pass_ticket=" + HttpUtility.UrlEncode(pass_ticket);
        }
        private static string GetParams(string devid, LoginXmlResult loginInfo, SendMsgInfo msgInfo, int scene)
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
                Msg = msgInfo,
                Scene = scene,
            };
            return JsonConvert.SerializeObject(param);
        }

        public static void GetData(Http http, string devid, LoginXmlResult loginInfo, SendMsgInfo msgInfo, int Scene, AjaxCallBack callback)
        {
            string url = GetUrl(loginInfo.pass_ticket);
            string param = GetParams(devid, loginInfo, msgInfo, Scene);
            http.PostDataAsync(url, param, callback);
        }
        public static string GetData(Http http, string devid, LoginXmlResult loginInfo, SendMsgInfo msgInfo, int Scene)
        {
            string url = GetUrl(loginInfo.pass_ticket);
            string param = GetParams(devid, loginInfo, msgInfo, Scene);
            var bytes = http.PostData(url, param);
            return http.Encoder.GetString(bytes);
        }
    }
}
