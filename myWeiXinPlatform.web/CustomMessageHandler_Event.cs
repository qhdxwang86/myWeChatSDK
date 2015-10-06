using myWeiXinPlatform.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.web
{
    public partial class CustomMessageHandler : MessageHandler<MessageContext>
    {
        protected override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这里写什么都无所谓，比如：上地爱你！";
            return responseMessage;
        }
        protected override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = @"您可以发送【位置】【图片】【语音】【视频】等类型的消息，您也可以直接单击菜单查看各种类型的消息";
            return responseMessage;
        }
        protected override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "有空再来";
            return responseMessage;
        }

        protected override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            IResponseMessageBase responseMessage = null;
            switch (requestMessage.EventKey)
            {
                case "OneClick":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您点击了底部按钮";
                        responseMessage = strongResponseMessage;
                    }
                    break;
                case "SubClickRoot_Text":
                    {
                        var strongResponseMessage = CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您点击了底部按钮";
                        responseMessage = strongResponseMessage;
                    }
                    break;

            }
            return responseMessage;
        }
        protected override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "感谢扫描";
            return responseMessage;
        }
    }
}
