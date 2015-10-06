using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace myWeiXinPlatform.common
{
   public interface IMessageHandler
    {
        string WeixinOpendId { get; }
        bool CancelExcute { get; set; }
        XDocument RequestDocument { get; set; }
        XDocument ResponseDocument { get;  }
        IRequestMessageBase RequestMessage { get; set; }
        IResponseMessageBase ResponseMessage { get; set; }

        void Execute();
        void OnExecuting();
        void OnExecuted();

    }
}
