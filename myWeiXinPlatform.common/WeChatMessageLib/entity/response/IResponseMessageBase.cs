using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
  public  interface IResponseMessageBase:IMessageBase
    {
        ResponseMsgType MsgType { get;}
    }
}
