using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
  public  class RequestMessageEvent_Scan:RequestMessageEventBase
    {
        public override Event Event
        {
            get
            {
                return Event.scan;
            }
        }
        public string Ticket { get; set; }
    }
}
