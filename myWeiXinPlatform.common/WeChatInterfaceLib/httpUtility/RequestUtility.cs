using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public static class RequestUtility
    {
        /// <summary>
        /// HTTPS  GET
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string  HttpGet(string url,Encoding encoding = null)
        {
            WebClient wc = new WebClient();
            wc.Encoding = encoding ?? Encoding.UTF8;
            return wc.DownloadString(url);
        }
        /// <summary>
        /// HTTPS POST
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postStream"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static  string   HttpPost(string url ,Stream  postStream,Encoding encoding = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postStream != null ? postStream.Length : 0;
            request.Accept = "text/html,application/xhtml+xml,application/xml";
            request.KeepAlive = true;
            request.UserAgent = "Mozilla/5.0 (Winodws NT 6.1; WOW64) AppleWebkit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            if (postStream != null)
            {
                postStream.Position = 0;
                Stream requestStream = request.GetRequestStream();
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead=postStream.Read(buffer,0,buffer.Length))!=0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }
                postStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.UTF8))
                {
                    string retString = myStreamReader.ReadToEnd();

                    return retString;
                }
            }
        }
    }
}
