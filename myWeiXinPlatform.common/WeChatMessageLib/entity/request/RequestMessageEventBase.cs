using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class RequestMessageEventBase : RequestMessageBase, IRequestMessageEventBase
    {
        public virtual Event Event
        {
            get
            {
                return Event.CLICK;
            }


        }

        public string EventKey
        {
            get; set;
        }

        public override RequestMsgType MsgType
        {
            get
            {
                return RequestMsgType.Event;
            }
        }
    }
}
