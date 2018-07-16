using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using FairyPay.PaymentProviders.Provider;
using Microsoft.Extensions.Primitives;

namespace FairyPay.PaymentProviders
{
    public static class Extensions
    {
        private static Encoding GBK = Encoding.GetEncoding("gbk");

        public static long UnixTimeSpan()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            return (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
        }

        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gbk_utf8(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return Encoding.UTF8.GetString(Encoding.Convert(GBK, Encoding.UTF8, Encoding.UTF8.GetBytes(text)));
        }

        /// <summary>
        /// UTF8转换成GB2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string utf8_gbk(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return GBK.GetString(Encoding.Convert(Encoding.UTF8, GBK, Encoding.UTF8.GetBytes(text)));
        }

        public static IEnumerable<KeyValuePair<string, StringValues>> AsKeyValuePairs(this NameValueCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Cast<string>().Select(key => new KeyValuePair<string, StringValues>(key, collection[key]));
        }

        public static NameValueCollection AsNameValueCollection(this IEnumerable<KeyValuePair<string, StringValues>> collection)
        {
            var values = new NameValueCollection();
            foreach (var pair in collection)
            {
                values.Add(pair.Key, pair.Value);
            }
            return values;
        }

        public static string JoinNvcToQs(this NameValueCollection qs, bool sort = false, bool encode = true,
            Func<string, string, bool> filter = null, Encoding encoding = null)
        {
            var keys = qs.AllKeys;

            if (filter != null)
            {
                keys = keys.Where(k => filter(k, qs[k])).ToArray();
            }
            if (sort)
            {
                Array.Sort(keys);
            }
            /*else
            {
                keys = qs.AllKeys.Where(k => !string.IsNullOrEmpty(qs[k])).ToArray();
            }*/
            if (encode)
            {
                if (encoding == null) encoding = Encoding.UTF8;
                return string.Join("&",
                    Array.ConvertAll(keys,
                        key => string.Format("{0}={1}", key, HttpUtility.UrlEncode(qs[key], encoding))));
            }
            return string.Join("&", Array.ConvertAll(keys, key => string.Format("{0}={1}", key, qs[key])));
        }

        public static string Post(this PaymentServicesProviderBase provider, string url, NameValueCollection data)
        {

            using (var stream = PostStream(provider, url, data))
            {
                using (StreamReader sr = new StreamReader(stream, provider.Encoding))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static Stream PostStream(this PaymentServicesProviderBase provider, string url,
            NameValueCollection data)
        {

            var query = string.Join("&",
                data.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(data[a], provider.Encoding)));
            return Post(provider, url, query, provider.Encoding);


        }

        public static Stream Post(this PaymentServicesProviderBase provider, string url, string data,
            Encoding encoding)
        {
            ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
            var requestContent = encoding.GetBytes(data);
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = true;
            request.ContentLength = requestContent.Length;
            request.Method = "POST";
            //request.ProtocolVersion = HttpVersion.Version11;
            //request.AllowAutoRedirect = true;
            //request.Headers.Add("X-Forwarded-For", "220.95.210.101");
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(requestContent, 0, requestContent.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            return response.GetResponseStream();
        }
    }
}