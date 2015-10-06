using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public interface IMessageContext
    {
       string UserName { get; set; }
        DateTime LastActiveTime { get; set; }
        MessageContainer<IRequestMessageBase> RequestMessages { get; set; }
        MessageContainer<IResponseMessageBase> ResponseMessages { get; set; }
        int MaxRecordCount { get; set; }
        object StorageData { get; set; }

        event EventHandler<WeixinContextRemovedArgs> MessageContextRemoved;

        void OnRemoved();
    }
}
