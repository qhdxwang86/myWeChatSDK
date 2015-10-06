using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class RequestMessageBase : MessageBase, IRequestMessageBase
    {
        public long MsgId
        {
            get; set;
        }

        public virtual RequestMsgType MsgType
        {
            get
            {
                return RequestMsgType.Text;
            }

        }
    }
}
