using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
   public class ResponseMessageVideo:ResponseMessageBase
    {
        public  new ResponseMsgType MsgType { get { return ResponseMsgType.Video; } }

        public ResponseMessageVideo()
        {
            Video = new Video();
        }
        public  Video Video { get; set; }
    }
    public  class Video
    {

        public string MediaId { get; set; }
        public string Title { get; set; }
        public      string Description { get; set; }
    }
}
