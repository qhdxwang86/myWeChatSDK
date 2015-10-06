using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class ResponseMessageNews : ResponseMessageBase
    {
        public new virtual ResponseMsgType MsgType
        {
            get
            {
                return ResponseMsgType.News;
            }
        }
        public ResponseMessageNews()
        {
            Articles = new List<Article>();
        }
        public  List<Article> Articles { get; set; }

        public int ArticleCount { get { return Articles.Count; } }
    }
    public class Article
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public string Url { get; set; }
    }
}
