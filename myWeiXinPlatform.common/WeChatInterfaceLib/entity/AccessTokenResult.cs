using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class AccessTokenResult
    {
        public  string access_token { get; set; }
        public int expire_in { get; set; }

    }

    public  class Token
    {

        public  static AccessTokenResult GetToken(string appid,string secret,string grant_type = "client_credential")
        {
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={2}&appid={0}&secret={1}", appid, secret, grant_type);
            string result = RequestUtility.HttpGet(url, null);
            return ApiHelper.GetResult<AccessTokenResult>(result);
        }

    }
}
