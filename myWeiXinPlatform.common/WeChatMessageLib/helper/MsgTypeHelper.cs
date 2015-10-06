using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace myWeiXinPlatform.common
{
    public class MsgTypeHelper
    {
        public  static  RequestMsgType GetRequestMsgType (XDocument doc)
        {
            return GetRequestMsgType(doc.Root.Element("MsgType").Value);
        }
        public  static  RequestMsgType GetRequestMsgType(string str)
        {

            return (RequestMsgType)Enum.Parse(typeof(RequestMsgType), str, true);
        }
    }
}
