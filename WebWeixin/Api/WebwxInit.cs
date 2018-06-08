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
    public class WebwxInit
    {
        private static string GetUrl(string pass_ticket,long rnd)
        {
            return "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxinit?r=" + rnd.ToString() + "&pass_ticket=" + HttpUtility.UrlEncode(pass_ticket);

        }
        private static string GetParams(string devid, LoginXmlResult loginInfo)
        {
            dynamic param= new
            {
                BaseRequest = new
                {
                    DeviceID = devid,
                    Sid = loginInfo.wxsid,
                    Skey = loginInfo.skey,
                    Uin = loginInfo.wxuin
                }
            };
            return JsonConvert.SerializeObject(param);
        }
        public static void GetInitData(Http http,string devid, long rnd, LoginXmlResult loginInfo, AjaxCallBack callback)
        {
            string url = GetUrl(loginInfo.pass_ticket, rnd);
            string param = GetParams(devid, loginInfo);
            http.PostDataAsync(url, param, callback);
        }
    }
}
