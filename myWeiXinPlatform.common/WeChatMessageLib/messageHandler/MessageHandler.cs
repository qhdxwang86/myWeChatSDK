using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace myWeiXinPlatform.common
{
    public class MessageHandler<TC> : IMessageHandler where TC : class, IMessageContext, new()
    {

        protected static WeixinContext<TC> GlobalWeixinContext = new WeixinContext<TC>();

        protected WeixinContext<TC> WeixinContext
        {
            get
            { return GlobalWeixinContext; }
        }

        public TC CurrentMessageContext
        {
            get { return WeixinContext.GetMessageContext(RequestMessage); }
        }

        public bool CancelExcute
        { get; set; }

        public IRequestMessageBase RequestMessage
        { get; set; }

        public XDocument RequestDocument
        { get; set; }

        public XDocument ResponseDocument
        {
            get
            {
                if (ResponseMessage == null)
                {
                    return null;
                }
                return EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase);
            }
        }

        public IResponseMessageBase ResponseMessage
        { get; set; }

        public string WeixinOpendId
        {
            get
            {
                if (RequestMessage != null)
                {
                    return RequestMessage.FromUserName;
                }
                return null;
            }
        }

        public MessageHandler(Stream inputStream,int maxRecordCount = 0) 
        {
            WeixinContext.MaxRecordCount = maxRecordCount;
            using (XmlReader xr=XmlReader.Create(inputStream))
            {
                RequestDocument = XDocument.Load(xr);
                Init(RequestDocument);
            }
        }
        public MessageHandler(XDocument requestDocument,int maxRecordCount=0)
        {
            WeixinContext.MaxRecordCount = maxRecordCount;
            Init(requestDocument);
        }

        public void Execute()
        {
            if (CancelExcute) {
                return;
            }
            OnExecuting();
            if (CancelExcute)
            {
                return;
            }
            try
            {
                if (RequestMessage == null) { return; }
                switch (RequestMessage.MsgType)
                {
                    case RequestMsgType.Text:
                        ResponseMessage = OnTextRequest(RequestMessage as RequestMessageText);
                        break;
                    case RequestMsgType.Location:
                        ResponseMessage = OnLocationRequest(RequestMessage as RequestMessageLocation);
                        break;
                    case RequestMsgType.Image:
                        ResponseMessage = OnImageRequest(RequestMessage as RequestMessageImage);
                        break;
                    case RequestMsgType.Voice:
                        ResponseMessage = OnVoiceRequest(RequestMessage as RequestMessageVoice);
                        break;
                    case RequestMsgType.Video:
                        ResponseMessage = OnVideoRequest(RequestMessage as RequestMessageVideo);
                        break;
                    case RequestMsgType.Link:
                        ResponseMessage = OnLinkRequest(RequestMessage as RequestMessageLink);
                        break;
                    case RequestMsgType.Event:
                        ResponseMessage = OnEventRequest(RequestMessage as RequestMessageEventBase);
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException("未知的MsgType");
                      
                }
                if (WeixinContextGlobal.UserWeixinContext && ResponseMessage != null)
                {
                    WeixinContext.InsertMessage(ResponseMessage);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                OnExecuted();
            }

        }

        public virtual void OnExecuted()
        {
            
        }

        public virtual  void OnExecuting()
        {
            
        }
        private  void Init(XDocument requestDocument)
        {
            RequestDocument = requestDocument;
            RequestMessage = EntityHelper.GetRequestEntity(RequestDocument);
            if (WeixinContextGlobal.UserWeixinContext)
            {
                WeixinContext.InsertMessage(RequestMessage);
            }
        }

        protected  TR CreateResponseMessage<TR>() where TR : ResponseMessageBase
        {
            if (RequestMessage == null)
            {
                return null;
            }
            return RequestMessage.CreateResponseMessage<TR>();
        }

        protected  virtual   IResponseMessageBase  DefaultResponseMessage (IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送的消息类型暂时未被识别";
            return responseMessage;
        }

        protected  virtual  IResponseMessageBase   OnTextRequest(RequestMessageText requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual  IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual IResponseMessageBase OnEventRequest(RequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.LOCATION:
                    responseMessage = OnEvent_LocationRequest(requestMessage as RequestMessageEvent_Location);
                    break;
                case Event.subscribe:
                    responseMessage = OnEvent_SubscribeRequest(requestMessage as RequestMessageEvent_Subscribe);
                    break;
                case Event.unsubscribe:
                    responseMessage = OnEvent_UnsubscribeRequest(requestMessage as RequestMessageEvent_Unsubscribe);
                    break;
                case Event.CLICK:
                    responseMessage = OnEvent_ClickRequest(requestMessage as RequestMessageEvent_Click);
                    break;
                case Event.scan:
                    responseMessage = OnEvent_ScanRequest(requestMessage as RequestMessageEvent_Scan);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event类型");
            }
            return responseMessage;
        }
        protected  virtual  IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

        protected virtual IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        protected virtual IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }

    }
}
