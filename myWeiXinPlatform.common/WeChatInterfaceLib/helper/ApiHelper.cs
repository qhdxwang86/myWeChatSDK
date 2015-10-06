using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace myWeiXinPlatform.common
{
    public static class ApiHelper
    {

        public  static  WxJsonResult  Get(string accessToken,string  urlFormat,params string[] querys)
        {
            return Get<WxJsonResult>(accessToken, urlFormat, querys);

        }

        public  static  T  Get<T>(string  accessToken,string  urlFormat,params string[] querys)
        {

            var url = GetApiUrl(urlFormat, accessToken, querys);
            string result = RequestUtility.HttpGet(url, null);
            return GetResult<T>(result);
        }


        public static  WxJsonResult Post(string  accessToken,string urlFormat,object data,params string[] querys)
        {
            return Post<WxJsonResult>(accessToken, urlFormat, data, querys);
        }

        public static T Post<T>(string accessToken, string urlFormat, object data, params string[] querys)
        {
            var url = GetApiUrl(urlFormat, accessToken, querys);
            JavaScriptSerializer js = new JavaScriptSerializer();
            var jsonString = js.Serialize(data);
            using (MemoryStream ms=new MemoryStream ())
            {
                var bytes = Encoding.UTF8.GetBytes(jsonString);
                ms.Write(bytes, 0, bytes.Length);
                ms.Seek(0, SeekOrigin.Begin);
                string result = RequestUtility.HttpPost(url, ms, null);
                return GetResult<T>(result);

            }
        }

        public static  string  GetApiUrl(string  urlFormat,string  accessToken,params string[] querys)
        {
            string[] args = new string[] { accessToken };
            if (querys.Length > 0)
            {
                args.Concat(querys).ToArray();
            }
            var url = string.Format(urlFormat, args);
            return url;
        }

        public  static  T GetResult<T>(string returnText)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            if (returnText.Contains("errorcode"))
            {
                WxJsonResult errorResult = js.Deserialize<WxJsonResult>(returnText);
                if (errorResult.errcode != ReturnCode.请求成功)
                {
                    throw new ErrorJsonResultException(string.Format("微信post请求发生错误，错误代码：{0},说明：{1}",
                        (int)errorResult.errcode, errorResult.errmsg), null, errorResult);
                }
               
            }
            T result = js.Deserialize<T>(returnText);

            return result;
        }
    }
}
