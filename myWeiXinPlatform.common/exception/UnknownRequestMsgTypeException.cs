using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class UnknownRequestMsgTypeException: WeixinException
    {
        public UnknownRequestMsgTypeException(string message) : base(message, null) { }
        public UnknownRequestMsgTypeException(string message, Exception inner) : base(message, inner) { }
    }
}
