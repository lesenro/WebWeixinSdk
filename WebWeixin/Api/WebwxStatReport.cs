using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebWeixin.Utils;

namespace WebWeixin.Api
{
    public class WebwxStatReport
    {
        private static string GetUrl(string pass_ticket = "")
        {
            return "https://wx.qq.com/cgi-bin/mmwebwx-bin/webwxstatreport?fun=new" + (string.IsNullOrEmpty(pass_ticket) ? "" : "&pass_ticket=" + HttpUtility.UrlEncode(pass_ticket));
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
            if (timeStamp == 0L)
            {
                return req;
            }
            dynamic appRuntime = new
            {
                type = "[app-runtime]",
                data = new
                {
                    unload = new
                    {
                        listenerCount = 117,
                        watchersCount = 111,
                        scopesCount = 28
                    }
                }
            };

            Random rand = new Random(Tools.GuidInt());
            dynamic appTiming = new
            {
                type = "[app-timing]",
                data = new
                {
                    appTiming = new
                    {
                        qrcodeStart = timeStamp + rand.Next(400, 500),
                        qrcodeEnd = timeStamp + rand.Next(500, 800),
                    },
                    pageTiming = new
                    {
                        navigationStart = timeStamp,
                        unloadEventStart = timeStamp + rand.Next(200, 220),
                        unloadEventEnd = timeStamp + rand.Next(220, 230),
                        redirectStart = 0,
                        redirectEnd = 0,
                        fetchStart = timeStamp,
                        domainLookupStart = timeStamp + rand.Next(80, 100),
                        domainLookupEnd = timeStamp + rand.Next(100, 120),
                        connectStart = timeStamp + rand.Next(100, 120),
                        connectEnd = timeStamp + rand.Next(160, 200),
                        secureConnectionStart = timeStamp + rand.Next(120, 160),
                        requestStart = timeStamp + rand.Next(160, 200),
                        responseStart = timeStamp + rand.Next(200, 220),
                        responseEnd = timeStamp + rand.Next(220, 230),
                        domLoading = timeStamp + rand.Next(230, 240),
                        domInteractive = timeStamp + rand.Next(240, 300),
                        domContentLoadedEventStart = timeStamp + rand.Next(240, 300),
                        domContentLoadedEventEnd = timeStamp + rand.Next(300, 310),
                        domComplete = timeStamp + rand.Next(310, 350),
                        loadEventStart = timeStamp + rand.Next(310, 350),
                        loadEventEnd = timeStamp + rand.Next(350, 400)
                    }
                }
            };

            req.List.Add(new
            {
                Type = 1,
                Text = JsonConvert.SerializeObject(appRuntime)
            });
            req.List.Add(new
            {
                Type = 1,
                Text = JsonConvert.SerializeObject(appTiming)
            });
            return req;
        }
        public static void GetData(Http http, string devid)
        {
            var report = GetParams(devid, 0L);
            http.PostAsync(GetUrl(), JsonConvert.SerializeObject(report));
        }
        public static void GetData(Http http, string devid,long timeStamp)
        {
            var report = GetParams(devid, timeStamp);
            http.PostAsync(GetUrl(), JsonConvert.SerializeObject(report));
        }

    }
}
