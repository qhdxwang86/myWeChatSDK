using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
   public class WxJsonResult
    {
        public ReturnCode errcode { get; set; }
        public  string errmsg { get; set; }
    }

    public  enum ReturnCode
    {
        请求成功=0
    }
}
