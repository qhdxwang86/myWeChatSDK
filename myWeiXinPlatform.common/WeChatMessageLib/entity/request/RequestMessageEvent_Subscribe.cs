using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class RequestMessageEvent_Subscribe : RequestMessageEventBase
    {
        public override Event Event
        {
            get
            {
                return Event.subscribe;
            }
        }
    }
}
