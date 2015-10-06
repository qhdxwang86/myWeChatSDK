using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class ResponseMessageBase : MessageBase, IResponseMessageBase
    {
        public virtual ResponseMsgType MsgType
        {
            get
            {
                return ResponseMsgType.Text;
            }
        }
    }
}
