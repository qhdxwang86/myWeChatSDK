using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
   public class CheckSignature
    {
        public const string Token = "CharlieLau";
        public  static  bool Check(string  signature,string timestamp,string nonce,string token=Token)
        {
            return signature == GetSignature( timestamp, nonce, token);

        }
        public static string  GetSignature(string timestamp,string nonce,string token)
        {
            token = token ?? Token;
            string[] arr = new [] { timestamp, nonce, token }.OrderBy(z=>z).ToArray();
            string arrString = string.Join("", arr);
            System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            byte[] sha1Arr= sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder sb = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
    }
}
