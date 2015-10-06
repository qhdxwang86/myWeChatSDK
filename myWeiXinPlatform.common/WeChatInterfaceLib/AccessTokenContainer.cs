using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{

    public class AccessTokenBag
    {
        public  string AppId { get; set; }
        public  string AppSecret { get; set; }
        public DateTime ExpireTime { get; set; }
        public AccessTokenResult AccessTokenResult { get; set; }
    }
    public class AccessTokenContainer
    {
        private static Dictionary<string, AccessTokenBag> AccessTokenCollection = new Dictionary<string, AccessTokenBag>(StringComparer.OrdinalIgnoreCase);

        public  static  string TryGetToken(string appId,string appSecret,bool getNewToken = false)
        {
            if (!CheckRegistered(appId) || getNewToken)
            {
                Register(appId, appSecret);
            }
            return GetToken(appId);
        }


        private  static void  Register(string appId,string appSecret)
        {

            AccessTokenCollection[appId] = new AccessTokenBag
            {
                AppId = appId,
                AppSecret = appSecret,
                ExpireTime = DateTime.MinValue,
                AccessTokenResult = new AccessTokenResult()
            };

        }
        private  static  bool  CheckRegistered(string appId)
        {
            return AccessTokenCollection.ContainsKey(appId);
        }
        private  static  string GetToken(string appId,bool getNewToken = false)
        {
            return GetTokenResult(appId, getNewToken).access_token;
        }

        private static AccessTokenResult  GetTokenResult(string appId,bool getNewToken = false)
        {
            if (!AccessTokenCollection.ContainsKey(appId))
            {
                throw new WeixinException("此appid尚未注册，请使用AccessTokenContainer.Regiter进行注册");
            }
            var accessTokenBag = AccessTokenCollection[appId];
            if (getNewToken || accessTokenBag.ExpireTime <= DateTime.Now)
            {
              accessTokenBag.AccessTokenResult=  Token.GetToken(accessTokenBag.AppId, accessTokenBag.AppSecret);
                accessTokenBag.ExpireTime = DateTime.Now.AddSeconds(accessTokenBag.AccessTokenResult.expire_in);
            }
            return accessTokenBag.AccessTokenResult;
        }
    }
}
