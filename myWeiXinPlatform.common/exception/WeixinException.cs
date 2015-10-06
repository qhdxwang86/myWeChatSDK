using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
   public class WeixinException:ApplicationException
    {
        public  WeixinException(string message) : base(message, null) { }
        public  WeixinException(string message,Exception inner) : base(message, inner) { }
    }
}
