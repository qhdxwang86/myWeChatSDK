using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class WeixinContext<TM> where TM : class, IMessageContext, new()
    {
        private const int DEFAULTEXPIREMINUTES = 90;

        protected Dictionary<string, TM> MessageCollection { get; set; }
        protected List<TM> MessageQueue { get; set; }
        public double ExpireMinutes { get; set; }
        public int MaxRecordCount { get; set; }
        public WeixinContext()
        {
            Restore();
        }
        public void Restore()
        {
            MessageCollection = new Dictionary<string, TM>(StringComparer.OrdinalIgnoreCase);
            MessageQueue = new List<TM>();
            ExpireMinutes = DEFAULTEXPIREMINUTES;
        }
        private TM GetMessageContext(string userName)
        {

            while (MessageQueue.Count > 0)
            {
                var firstMessageContext = MessageQueue[0];
                var timeSpan = DateTime.Now - firstMessageContext.LastActiveTime;
                if (timeSpan.TotalMinutes >= ExpireMinutes)
                {
                    MessageQueue.RemoveAt(0);
                    MessageCollection.Remove(firstMessageContext.UserName);
                    firstMessageContext.OnRemoved();
                }
                else
                {
                    break;
                }
            }
            if (!MessageCollection.ContainsKey(userName))
            {
                return null;
            }
            return MessageCollection[userName];
        }

        private TM GetMessageContext(string userName, bool createIfNotExists)
        {
            var messageContext = GetMessageContext(userName);
            if (messageContext == null)
            {

                if (createIfNotExists)
                {
                    MessageCollection[userName] = new TM()
                    {
                        UserName = userName,
                        MaxRecordCount = MaxRecordCount
                    };
                    messageContext = GetMessageContext(userName);
                    MessageQueue.Add(messageContext);
                }
                else
                {
                    return null;
                }
            }
            return messageContext;
        }
    
        public  TM  GetMessageContext(IRequestMessageBase requestMessage)
        {
            lock (WeixinContextGlobal.Lock)
            {
                return GetMessageContext(requestMessage.FromUserName);
            }
        }

        public TM GetMessageContext(IResponseMessageBase responseMessage)
        {
            lock (WeixinContextGlobal.Lock)
            {
                return GetMessageContext(responseMessage.ToUserName, true);
            }
        }
        public  void  InsertMessage(IRequestMessageBase requestMessage)
        {

            lock (WeixinContextGlobal.Lock)
            {
                var userName = requestMessage.FromUserName;
                var messageContext = GetMessageContext(userName, true);
                if (messageContext.RequestMessages.Count > 0)
                {
                    var messageContextInQueue = MessageQueue.FindIndex(z => z.UserName == userName);
                    if (messageContextInQueue >= 0)
                    {
                        MessageQueue.RemoveAt(messageContextInQueue);
                        MessageQueue.Add(messageContext);
                    }
                }
                messageContext.LastActiveTime = DateTime.Now;
                messageContext.RequestMessages.Add(requestMessage);
            }
        }

        public void   InsertMessage(IResponseMessageBase responseMessage)
        {
            lock (WeixinContextGlobal.Lock)
            {
                var messageContext = GetMessageContext(responseMessage.ToUserName);
                messageContext.ResponseMessages.Add(responseMessage);
            }
        }

        public  IRequestMessageBase  GetLastRequestMessage(string userName)
        {
            lock (WeixinContextGlobal.Lock)
            {
                var messageContext = GetMessageContext(userName, true);
                return messageContext.RequestMessages.LastOrDefault();
            }
        }

        public IResponseMessageBase GetLastResponseMessage(string userName)
        {
            lock (WeixinContextGlobal.Lock)
            {
                var messageContext = GetMessageContext(userName, true);
                return messageContext.ResponseMessages.LastOrDefault();
            }
        }
    }
}
