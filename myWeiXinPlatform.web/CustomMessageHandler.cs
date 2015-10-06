using myWeiXinPlatform.common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.web
{
    public partial class CustomMessageHandler : MessageHandler<MessageContext>
    {
        public CustomMessageHandler(Stream inputStream, int maxRecordCount) : base(inputStream, maxRecordCount)
        {
            WeixinContext.ExpireMinutes = 3;
        }
        public override void OnExecuting()
        {
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 3;
            }
            base.OnExecuting();
        }
        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = (int)CurrentMessageContext.StorageData + 1;
        }

        protected override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();

            var result = new StringBuilder();
            result.AppendFormat("您刚才发送了文字信息：{0}\r\n\r\n", requestMessage.Content);
            if (CurrentMessageContext.RequestMessages.Count > 1)
            {
                result.AppendFormat("您刚才还发送了如下消息,{0}/{1}:\r\n",
                    CurrentMessageContext.RequestMessages.Count, CurrentMessageContext.StorageData);
                for (int i = CurrentMessageContext.RequestMessages.Count - 2; i >= 0; i--)
                {
                    var historyMessage = CurrentMessageContext.RequestMessages[i];
                    result.AppendFormat("{0} 【{1}】 {2} \r\n", historyMessage.CreateTime.ToShortTimeString(), historyMessage.MsgType.ToString(),
                        (historyMessage is RequestMessageText) ? (historyMessage as RequestMessageText).Content : "非文字消息");

                }
                result.Append("\r\n");
            }
            result.AppendFormat("如果您在{0}分钟内连续发送消息，记录将被自动保留（当前设置：最多{1}条）。过期后记录将会自动清除。\r\n", WeixinContext.ExpireMinutes, WeixinContext.MaxRecordCount);
            result.AppendLine("\r\n");
            result.AppendLine("您还可以发送【位置】【图片】【语音】【视频】等类型的消息（注意这几种类型，不是这几种文字）,查看不同格式回复");
            responseMessage.Content = result.ToString();
            return responseMessage;
        }

        protected override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = string.Format("您刚才发送的地理位置信息。Location_X:{0},Location_Y:{1},Scale{2},标签:{3}",
                requestMessage.Location_X, requestMessage.Location_Y, requestMessage.Scale, requestMessage.Label);
            return responseMessage;
        }
        protected override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageNews>();
            responseMessage.Articles.Add(new Article()
            {
                Title = "您刚才发送的图片信息",
                Description = "您发送的图片将会显示在左边",
                PicUrl = requestMessage.PicUrl,
                Url = "http://www.baidu.com"
            });
            responseMessage.Articles.Add(new Article()
            {
                Title = "第二条",
                Description = "第二条带链接内容",
                PicUrl = requestMessage.PicUrl,
                Url = "http://www.baidu.com"
            });
            return responseMessage;
        }
        protected override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageMusic>();
            responseMessage.Music.MusicUrl = "http://www.qxuninfo.com/music.mp3";
            responseMessage.Music.Title = "这是一条音乐消息";
            responseMessage.Music.Description = "时间都去哪儿";
            return responseMessage;
        }
        protected override IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送了一条视频信息，id:" + requestMessage.MediaId;
            return responseMessage;
        }
        protected override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = string.Format(@"您发送一条链接信息：Title:{0} Description{1} Url{2}",
                requestMessage.Title, requestMessage.Description, requestMessage.Url);
            return responseMessage;
        }
        protected override IResponseMessageBase OnEventRequest(RequestMessageEventBase requestMessage)
        {
            var eventResponseMessage = base.OnEventRequest(requestMessage);
            return eventResponseMessage;
        }
        protected override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这条消息来自 DefaultResponseMessage";
            return responseMessage;
        }
    }
}
