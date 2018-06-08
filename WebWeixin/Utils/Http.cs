using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebWeixin.Utils
{
    public delegate void AjaxCallBack(string result);
    public delegate void FileDownHandle(byte[] fileBytes);

    public class Http
    {
        public WebHeaderCollection Headers { get; set; }
        public Encoding Encoder { get; set; }
        public string Referer { get; set; }
        public CookieContainer Cookies { get; set; } = new CookieContainer();
        public int Timeout { get; set; }
        public Http()
        {
            Headers = new WebHeaderCollection();
            Encoder = Encoding.UTF8;
            Referer = "";
            Timeout = 3000;
        }
        public string Get(string Url)
        {
            WebClientExt client = GetWebClient();
            string result = client.DownloadString(Url);
            return result;
        }
        public void GetAsync(string Url, AjaxCallBack callback=null)
        {
            WebClientExt client = GetWebClient();
            client.DownloadStringCompleted += Client_DownloadStringCompleted; ;
            client.DownloadStringAsync(new Uri(Url), callback);
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            WebClientExt client = sender as WebClientExt;

            if (e.UserState != null)
            {
                var callback = e.UserState as AjaxCallBack;
                callback(e.Result);
            }
        }

        public byte[] GetData(string Url)
        {

            WebClientExt client = GetWebClient();
            var result = client.DownloadData(Url);
            return result;
        }
        public void GetDataAsync(string Url, FileDownHandle callback)
        {

            WebClientExt client = GetWebClient();
            client.DownloadDataCompleted += (object sender, DownloadDataCompletedEventArgs e) =>
            {
                callback?.Invoke(e.Result);
            };
            client.DownloadDataAsync(new Uri(Url));
        }



        public string Post(string Url, string Data)
        {
            WebClientExt client = GetWebClient();

            string result = client.UploadString(Url, Data);

            return result;
        }

        public void PostAsync(string Url, string Data, AjaxCallBack callback=null)
        {
            WebClientExt client = GetWebClient();

            client.UploadStringCompleted += Client_UploadStringCompleted;
            client.UploadStringAsync(new Uri(Url), "POST", Data, callback);
        }

        private void Client_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            WebClientExt client = sender as WebClientExt;

            if (e.UserState != null)
            {
                var callback = e.UserState as AjaxCallBack;
                callback(e.Result);
            }
        }
        public byte[] PostData(string Url, string Data)
        {
            

            WebClientExt client = GetWebClient();

            var result = client.UploadData(Url, Encoder.GetBytes(Data));
            return result;
        }
        
        public void PostDataAsync(string Url, string Data, AjaxCallBack callback = null)
        {
            WebClientExt client = GetWebClient();

            client.UploadDataCompleted += Client_UploadDataCompleted;
            client.UploadDataAsync(new Uri(Url), "POST", Encoder.GetBytes(Data), callback);
        }
        public void PostDataAsync(string Url, byte[] Data, AjaxCallBack callback = null)
        {
            WebClientExt client = GetWebClient();

            client.UploadDataCompleted += Client_UploadDataCompleted;
            client.UploadDataAsync(new Uri(Url), "POST", Data, callback);
        }

        private void Client_UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
        {
            WebClientExt client = sender as WebClientExt;

            if (e.UserState != null)
            {
                var callback = e.UserState as AjaxCallBack;
                string result = Encoder.GetString(e.Result);
                callback(result);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private WebClientExt GetWebClient()
        {
            WebClientExt client = new WebClientExt();
            client.CookieContainer = Cookies;
            client.Timeout = Timeout;
            client.Encoding = Encoder;
            Headers.Set(HttpRequestHeader.Referer, Referer);
            client.Headers = Headers;


            return client;
        }
    }
}
