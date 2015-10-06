using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class WeixinContextGlobal
    {
        public static object Lock = new object();
        public static bool UserWeixinContext = true;
    }

    public class WeixinContextRemovedArgs : EventArgs
    {
        public string OpenId { get; set; }
        public DateTime LastActiveTime { get { return MessageContext.LastActiveTime; } }
        public IMessageContext MessageContext { get; set; }
        public WeixinContextRemovedArgs(IMessageContext messagecontext)
        {
            MessageContext = messagecontext;
        }
    }
}
