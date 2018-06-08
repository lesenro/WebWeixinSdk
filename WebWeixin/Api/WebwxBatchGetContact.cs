using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWeixin.Models;
using WebWeixin.Utils;

namespace WebWeixin.Api
{
    public class WebwxBatchGetContact
    {
        private static string GetUrl(string pass_ticket, string skey)
        {
            return "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxbatchgetcontact?type=ex&r=" + Tools.Timestamp().ToString() +
                "&lang=zh_CN&pass_ticket=" + pass_ticket;
        }
        private static string GetParams(string devid, LoginXmlResult loginInfo, List<Contact> list)
        {
            List<dynamic> nlist = new List<dynamic>();
            list.ForEach(x =>
            {
                nlist.Add(new
                {
                    EncryChatRoomId = x.EncryChatRoomId,
                    UserName = x.UserName,
                });
            });
            dynamic param = new
            {
                BaseRequest = new
                {
                    DeviceID = devid,
                    Sid = loginInfo.wxsid,
                    Skey = loginInfo.skey,
                    Uin = loginInfo.wxuin
                },
                Count = nlist.Count,
                List = nlist,
            };
            return JsonConvert.SerializeObject(param);
        }
        public static void GetData(Http http, string devid, LoginXmlResult loginInfo, List<Contact> list, AjaxCallBack callback)
        {
            string url = GetUrl(loginInfo.pass_ticket, loginInfo.skey);
            string param = GetParams(devid, loginInfo, list);
            http.PostDataAsync(url, param, callback);
        }
        public static string GetData(Http http, string devid, LoginXmlResult loginInfo, List<Contact> list)
        {
            string url = GetUrl(loginInfo.pass_ticket, loginInfo.skey);
            string param = GetParams(devid, loginInfo, list);
            var bytes = http.PostData(url, param);
            return http.Encoder.GetString(bytes);
        }
    }
}
