using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class MessageContext : IMessageContext
    {
        public DateTime LastActiveTime
        {
            get; set;
        }
        public MessageContainer<IRequestMessageBase> RequestMessages
        {
            get; set;
        }
        public MessageContainer<IResponseMessageBase> ResponseMessages
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
        public object StorageData
        {
            get; set;
        }
        private int _maxRecordCount;
        public int MaxRecordCount
        {
            get
            {
                return _maxRecordCount;
            }

            set
            {
                RequestMessages.MaxRecordCount = value;
                ResponseMessages.MaxRecordCount = value;
                _maxRecordCount = value;
            }
        }

        public event EventHandler<WeixinContextRemovedArgs> MessageContextRemoved = null;

        public MessageContext()
        {
            RequestMessages = new MessageContainer<IRequestMessageBase>(MaxRecordCount);
            ResponseMessages = new MessageContainer<IResponseMessageBase>(MaxRecordCount);
            LastActiveTime = DateTime.Now;

        }
       

        public virtual void OnRemoved()
        {
            var onRemovedArg = new WeixinContextRemovedArgs(this);
            OnMessageContextRemoved(onRemovedArg);
        }
        public void  OnMessageContextRemoved(WeixinContextRemovedArgs e)
        {
            EventHandler<WeixinContextRemovedArgs> tmp = MessageContextRemoved;
            if (tmp != null)
            {
                tmp(this, e);
            }

        }
    }
}
