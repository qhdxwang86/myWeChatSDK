using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
 public   class RequestMessageImage:RequestMessageBase
    {
        public override RequestMsgType MsgType
        {
            get
            {
                return RequestMsgType.Image;
            }
        }
        public string MediaId { get; set; }
        public  string PicUrl { get; set; }
    }
}
