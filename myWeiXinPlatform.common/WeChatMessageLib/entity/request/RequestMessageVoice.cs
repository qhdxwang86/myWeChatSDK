using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class RequestMessageVoice : RequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get
            {
                return RequestMsgType.Video;
            }
        }
        public string Format { get; set; }
        public string Recognition { get; set; }
    }
}
