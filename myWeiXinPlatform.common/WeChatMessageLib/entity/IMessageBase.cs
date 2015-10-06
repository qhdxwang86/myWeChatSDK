using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public interface IMessageBase
    {
        string FromUserName { get; set; }
        string ToUserName { get; set; }
        DateTime CreateTime { get; set; }
    }
}
