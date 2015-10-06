using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
   public class RequestMessageVideo:RequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get
            {
                return RequestMsgType.Video;
            }
        }
        public  string MediaId { get; set; }

        public string ThumbMediaId { get; set; }
    }
}
