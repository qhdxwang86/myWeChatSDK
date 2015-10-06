using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
  public  class ErrorJsonResultException:WeixinException
    {
        public WxJsonResult   JsonResult { get; set; }

        public ErrorJsonResultException(string message,Exception inner,WxJsonResult jsonResult)
            :base(message,inner)
        {
            JsonResult = jsonResult;
        }
    }
}
