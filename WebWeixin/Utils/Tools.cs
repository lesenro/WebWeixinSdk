using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WebWeixin.Utils
{
    public class Tools
    {
        public static long Timestamp()
        {
            return Timestamp(DateTime.Now);
        }
        public static long Timestamp(DateTime now)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            return (long)(now - startTime).TotalMilliseconds;
        }
        public static long RandomNumber(int len)
        {
            long lnum = 1L;
            for (int i = 0; i < len; i++)
            {
                lnum = lnum * 10;
            }
            Random rand = new Random(GuidInt());
            return (long)(rand.NextDouble() * lnum);
        }
        public static string FixLen(string source ,int len, char fixchar=' ', bool left=true)
        {
            if (source.Length > len)
            {
                int fix = source.Length - len;
                if (left)
                {
                    source = source.Substring(0, len);
                }
                else
                {
                    source = source.Substring(fix);
                }
            }
            else if (source.Length < len)
            {
                int fix = len - source.Length;
                if (left)
                {
                    source = (new string(fixchar,fix))+ source;
                }
                else
                {
                    source = source + (new string(fixchar, fix));
                }
            }
            return source;
        }
        public static int GuidInt()
        {
            return Guid.NewGuid().GetHashCode();
        }
        public static Dictionary<string, string> QueryStringToDict(string query)
        {
            //?ticket=Adl5JYQF8gJLsQLnj8ZrqUbQ@qrticket_0&uuid=wfZiucrrMw==&lang=zh_CN&scan=1527066406
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string q in query.Split("&".ToArray()))
            {
                var s = q.Split("=".ToArray());
                string key = s[0].Replace("?", "").Trim();
                if (s.Length == 2)
                {
                    dict.Add(key, s[1]);
                }
                else
                {
                    dict.Add(key, "");
                }
            }
            return dict;
        }
        public static T XmlDeserialize<T>(string xml) where T : new()
        {
            return XmlDeserialize<T>(xml, Encoding.UTF8);
        }
        public static T XmlDeserialize<T>(string xml, Encoding encoding) where T : new()
        {
            try
            {
                var mySerializer = new XmlSerializer(typeof(T));
                using (var ms = new MemoryStream(encoding.GetBytes(xml)))
                {
                    using (var sr = new StreamReader(ms, encoding))
                    {
                        return (T)mySerializer.Deserialize(sr);
                    }
                }
            }
            catch (Exception e)
            {
                return default(T);
            }

        }
    }
}
