using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using WebWeixin.Api;
using WebWeixin.Models;
using WebWeixin.Utils;

namespace WebWeixin
{
    public class WeiXinSdk
    {
        public string Uuid { get; set; } = "";
        public string DeviceID { get; set; }
        public string Ticket { get; set; }
        public string Scan { get; set; }
        public LoginXmlResult LoginInfo { get; set; }
        public User UserInfo { get; set; }



        public delegate void ImageChangedHandle(Image img);
        public event ImageChangedHandle ImageChanged;

        public delegate void AjaxHandle(AjaxResult result);
        public event AjaxHandle AjaxCompleted;
        private long StartTimestamp { get; set; }
        private int TimestampCount { get; set; }
        private Http httpClient, httpJson, httpFile;
        private SyncKeyData SyncKey { get; set; }
        private void httpInit()
        {
            CookieCollection cookie = new CookieCollection();
            cookie.Add(new Cookie("refreshTimes", "5", "/", "wx.qq.com"));
            cookie.Add(new Cookie("mm_lang", "zh_CN", "/", "wx.qq.com"));
            cookie.Add(new Cookie("MM_WX_NOTIFY_STATE", "1", "/", "wx.qq.com"));
            cookie.Add(new Cookie("MM_WX_SOUND_STATE", "1", "/", "wx.qq.com"));

            httpClient = new Http();
            httpClient.Referer = "https://wx.qq.com/";
            httpClient.Encoder = Encoding.UTF8;

            httpClient.Cookies.Add(cookie);

            httpClient.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            httpClient.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
            httpClient.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            httpClient.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
            httpClient.Headers.Add(HttpRequestHeader.Connection, "Connection");
            httpClient.Headers.Add("Upgrade-Insecure-Requests", "1");
            httpClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36");


            httpJson = new Http();
            httpJson.Referer = "https://wx.qq.com/";
            httpJson.Encoder = Encoding.UTF8;

            httpJson.Cookies.Add(cookie);

            httpJson.Headers.Add(HttpRequestHeader.Accept, "application/json, text/plain, */*");
            httpJson.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            httpJson.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            //httpJson.Headers.Add(HttpRequestHeader.Connection, "keep-alive");
            httpJson.Headers.Add(HttpRequestHeader.ContentType, "application/json;charset=UTF-8");
            httpJson.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36");

            httpFile = new Http();
            httpFile.Referer = "https://wx.qq.com/";

            httpFile.Cookies.Add(cookie);

            httpFile.Headers.Add(HttpRequestHeader.Accept, "image/webp,image/*,*/*;q=0.8");
            httpFile.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
            httpFile.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            httpFile.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36");

        }
        /// <summary>
        /// 初始化
        /// </summary>
        public WeiXinSdk()
        {
            httpInit();
            Ticket = "";
            Scan = "";
            int len = 15;
            string devnum = Tools.RandomNumber(15).ToString();

            if (devnum.Length < len)
            {
                devnum = (new string('0', len - devnum.Length)) + devnum;
            }
            DeviceID = "e" + devnum;
            TimestampCount = 0;
        }
        /// <summary>
        /// 登录时二维码 和用户头像
        /// </summary>
        /// <param name="img"></param>
        private void OnImageChanged(Image img)
        {
            ImageChanged?.Invoke(img);
        }
        /// <summary>
        /// 响应异步调用事件
        /// </summary>
        /// <param name="result"></param>
        private void OnAjaxCompleted(AjaxResult result)
        {
            AjaxCompleted?.Invoke(result);
        }
        //登录
        public void StartLogin()
        {
            //开始时间戳
            StartTimestamp = Tools.Timestamp();
            //获取uuid
            Uuid = LoginUuid.GetUuid(StartTimestamp, httpClient);
            TimestampCount++;
            //报告设备号，无实际意义，模拟真实页面
            WebwxStatReport.GetData(httpJson, DeviceID);
            //开始监听登录事件
            httpClient.GetAsync(LoginQrcode.MonitorUrl(Uuid, StartTimestamp + TimestampCount++, GetRnum()), WaitScanCode);
            //报告时设备号，时间戳，无实际意义，模拟真实页面
            WebwxStatReport.GetData(httpJson, DeviceID, StartTimestamp);
            //获取登录二维码
            OnImageChanged(LoginQrcode.QrCode(httpClient, Uuid));
        }
        public void DownFile(string url, FileDownHandle callback)
        {
            if (!url.ToLower().Contains("://"))
            {
                url = "https://wx.qq.com" + url;
            }
            httpFile.GetDataAsync(url, callback);
        }
        //监听二维码扫描状态
        private void WaitScanCode(string result)
        {
            //等待中
            //window.code = 201
            if (result.Trim().Contains("window.code=408"))
            {
                httpClient.GetAsync(LoginQrcode.MonitorUrl(Uuid, StartTimestamp + TimestampCount++, GetRnum()), WaitScanCode);
            }
            else
            //二维码失效
            if (result.Trim().Contains("window.code=400"))
            {

            }
            else
            //扫码完成
            if (result.Trim().Contains("window.code=201"))
            {
                var bytes = LoginQrcode.UserAvatar(result);
                if (bytes.Length > 0)
                {
                    MemoryStream stream = new MemoryStream(bytes);
                    OnImageChanged(Image.FromStream(stream));
                }
                else
                {
                    //没有头像
                }
                httpClient.GetAsync(LoginQrcode.MonitorUrl(Uuid, StartTimestamp + TimestampCount++, GetRnum()), WaitScanCode);
            }
            else
            //确认登录完成
            if (result.Trim().Contains("window.code=200"))
            {
                string r_url = LoginQrcode.LoginRedirectUri(result);
                Uri uri = new Uri(r_url);
                var dict = Tools.QueryStringToDict(uri.Query);
                Ticket = dict["ticket"] ?? "";
                Scan = dict["scan"] ?? "";
                LoginCompleted(r_url);
            }
        }
        //开始同步检测
        public void StartSyncCheck(string result)
        {
            
            if (!result.Contains("selector:\"0\"")||!result.Contains("retcode:\"0\""))
            {
                WebwxSync.GetData(httpJson, DeviceID, LoginInfo, SyncKey, GetRnum(), (string json) =>
                {
                    WebwxSyncInfo syncInfo = JsonConvert.DeserializeObject<WebwxSyncInfo>(json);
                    //更新同步key
                    this.SyncKey = syncInfo.SyncKey;
                    AjaxResult syncResult = new AjaxResult
                    {
                        AjaxType = AjaxTypes.WxSync,
                        Data = syncInfo,
                    };

                    OnAjaxCompleted(syncResult);
                    WebwxSyncCheck.GetData(httpClient, DeviceID, LoginInfo, SyncKey, StartTimestamp + TimestampCount++, StartSyncCheck);
                });
            }
            else
            {
                WebwxSyncCheck.GetData(httpClient, DeviceID, LoginInfo, SyncKey, StartTimestamp + TimestampCount++, StartSyncCheck);
            }
        }
        //登录完成
        public void LoginCompleted(string url)
        {
            url = url + "&fun=new&version=v2&lang=zh_CN";
            var byts = httpJson.GetData(url);
            string loginXml = Encoding.UTF8.GetString(byts);
            LoginInfo = Tools.XmlDeserialize<LoginXmlResult>(loginXml);
            //更新httpClient
            httpClient.Cookies = httpJson.Cookies;
            httpClient.Headers[HttpRequestHeader.Accept] = "*/*";
            httpClient.Headers.Remove(HttpRequestHeader.Connection);
            //更新文件下载httpFile
            httpFile.Cookies = httpJson.Cookies;
            //更新httpJsonCookie
            cookieFix();

            //获取初始化信息
            WebwxInit.GetInitData(httpJson, DeviceID, GetRnum(), LoginInfo, (string json) =>
            {
                var initInfo = JsonConvert.DeserializeObject<WebwxInitInfo>(json);
                SyncKey = initInfo.SyncKey;
                UserInfo = initInfo.User;
                //初始化信息
                AjaxResult result = new AjaxResult
                {
                    AjaxType = AjaxTypes.WxInit,
                    Data = initInfo,
                };

                OnAjaxCompleted(result);
                //发通知，确认为已读
                SendStatusNotify(3, UserInfo.UserName, UserInfo.UserName);
                //获取联系人
                WebwxGetContact.GetData(httpJson, LoginInfo, (string contactJson) => {
                    AjaxResult contactResult = new AjaxResult
                    {
                        AjaxType = AjaxTypes.WxContact,
                        Data = JsonConvert.DeserializeObject<WebwxContactInfo>(contactJson),
                    };

                    OnAjaxCompleted(contactResult);
                });

                //开始同步检测
                StartSyncCheck("selector:\"0\"");

            });
        }
        //批量获取用户信息
        public void BatchGetContact(List<Contact> list,string userName)
        {
            //发送状态信息
            WebwxBatchGetContact.GetData(httpJson, DeviceID, LoginInfo, list, (string json) => {
                AjaxResult result = new AjaxResult
                {
                    AjaxType = AjaxTypes.WxBatchGetContact,
                    Data = JsonConvert.DeserializeObject<WebwxContactInfo>(json),
                    DataKey=userName,
                };
                OnAjaxCompleted(result);
            });

        }
        //发通知，确认为已读
        public void SendStatusNotify(int code, string fromUser, string toUser)
        {
            //发送状态信息
            WebwxStatusNotify.GetData(httpJson, DeviceID, LoginInfo, code, fromUser, toUser, (string statusJson) => {
                AjaxResult statusResult = new AjaxResult
                {
                    AjaxType = AjaxTypes.WxStatusNotify,
                    Data = JsonConvert.DeserializeObject<WebwxStatusNotifyInfo>(statusJson),
                };
                OnAjaxCompleted(statusResult);
            });
        }
        //发送消息
        public SendMsgResult SendMsg(string toUser, string content)
        {
            string fromUser = UserInfo.UserName;
            string msgid = Tools.RandomNumber(4).ToString();
            msgid = Tools.FixLen(msgid, 4, '0');
            msgid = Tools.Timestamp().ToString() + msgid;
            SendMsgInfo msgInfo = new SendMsgInfo
            {
                Content = content,
                ToUserName = toUser,
                FromUserName = fromUser,
                Type = 1,
                ClientMsgId = msgid,
                LocalID = msgid,
            };
            string json = WebwxSendMsg.GetData(httpJson, DeviceID, LoginInfo, msgInfo, 0);
            return JsonConvert.DeserializeObject<SendMsgResult>(json);
        }
        private long GetRnum()
        {
            long endTime = 2000000000 + 3600000;
            endTime = 1529008357117;
            long today = Tools.Timestamp(DateTime.Parse(DateTime.Now.ToLongDateString()));
            //return endTime - (Tools.Timestamp() - today);
            return endTime - Tools.Timestamp();
        }
        private void cookieFix()
        {

            httpJson.Cookies.Add(new Cookie("login_frequency", "1", "/", "wx.qq.com"));
            httpJson.Cookies.Add(new Cookie("last_wxuin", LoginInfo.wxuin, "/", "wx.qq.com"));

        }
        private void logs(string txt,int level=0)
        {
            string l = DateTime.Now.ToString() + ":" + txt;
            Console.WriteLine(l);
        }
    }
    
}
