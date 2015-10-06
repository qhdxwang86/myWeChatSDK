using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class ResponseMessageImage:ResponseMessageBase
    {
        public new   virtual   ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Image; }
        }

        public ResponseMessageImage()
        {
            Image = new Image();
        }
        public  Image Image { get; set; }

    }
    public  class Image
    {
        public string MediaId { get; set; }
    }
}
