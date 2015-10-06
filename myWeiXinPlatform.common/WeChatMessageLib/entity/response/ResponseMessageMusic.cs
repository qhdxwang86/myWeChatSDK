using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
   public class ResponseMessageMusic:ResponseMessageBase
    {
        public new  ResponseMsgType MsgType
        {

            get { return ResponseMsgType.Music; }
        }

        public ResponseMessageMusic()
        {
            Music = new Music();
        }
        public  Music Music { get; set; }
    }
    public class Music
    {
        public  string Title { get; set; }

        public string Description { get; set; }

        public string MusicUrl { get; set; }
        public string HQMusicUrl { get; set; }

        public  string ThumbMediaId { get; set; }
    }
}
