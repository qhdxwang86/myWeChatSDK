using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
   public class ResponseMessageVoice:ResponseMessageBase
    {
        public new  virtual  ResponseMsgType MsgType
        {

            get { return ResponseMsgType.Voice; }
        }
        public ResponseMessageVoice()
        {
            Voice = new Voice();
        }
        public  Voice Voice { get; set; }

    }

    public  class Voice
    {
        public string MediaId { get; set; }
    }
}
