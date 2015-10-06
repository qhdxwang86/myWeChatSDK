using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class RequestMessageLink : RequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get
            {
                return RequestMsgType.Link;
            }
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }
}
