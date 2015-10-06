using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class MessageBase
    {
        public  string ToUserName { get; set; }
        public  string FromUserName { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
