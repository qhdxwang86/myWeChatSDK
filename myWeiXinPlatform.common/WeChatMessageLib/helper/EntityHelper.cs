using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace myWeiXinPlatform.common
{
    public static class EntityHelper
    {
        public static IRequestMessageBase GetRequestEntity(XDocument doc)
        {

            RequestMessageBase requestMessage = null;
            RequestMsgType msgType;
            try
            {
                msgType = MsgTypeHelper.GetRequestMsgType(doc);

                switch (msgType)
                {
                    case RequestMsgType.Text:
                        requestMessage = new RequestMessageText();
                        break;
                    case RequestMsgType.Location:
                        requestMessage = new RequestMessageLocation();
                        break;
                    case RequestMsgType.Image:
                        requestMessage = new RequestMessageImage();
                        break;
                    case RequestMsgType.Voice:
                        requestMessage = new RequestMessageVoice();
                        break;
                    case RequestMsgType.Link:
                        requestMessage = new RequestMessageLink();
                        break;
                    case RequestMsgType.Event:
                       
                        Event eventType = EventHelper.GetEventType(doc);

                        switch (eventType)
                        {
                            case Event.LOCATION:
                                requestMessage = new RequestMessageEvent_Location();
                                break;
                            case Event.subscribe:
                                requestMessage = new RequestMessageEvent_Subscribe();
                                break;
                            case Event.unsubscribe:
                                requestMessage = new RequestMessageEvent_Unsubscribe();
                                break;
                            case Event.CLICK:
                                requestMessage = new RequestMessageEvent_Click();
                                break;
                            case Event.scan:
                                requestMessage = new RequestMessageEvent_Scan();
                                break;
                            default:
                                requestMessage = new RequestMessageEventBase();
                                break;
                        }
                        break;
                    default:

                        throw new UnknownRequestMsgTypeException(string.Format("UnknownRequestMsgTypeException"), new ArgumentOutOfRangeException());
                }

                FillEntityWithXml(requestMessage,doc);

                return requestMessage;
            }
            catch (ArgumentException ex)
            {
                throw new WeixinException(string.Format("WeixinException:{0}", ex));
                throw;
            }
        }

        public static void FillEntityWithXml<T>(this T entity, XDocument doc) where T : class, new()
        {
            entity = entity ?? new T();
            var root = doc.Root;
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var xmlPropVal = root.Element(propName).Value;
                if (root.Element(propName) != null)
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "DateTime":
                            prop.SetValue(entity, DatetimeHelper.GetDateTimeFromXml(xmlPropVal), null);
                            break;
                        case "Int32":
                            prop.SetValue(entity, int.Parse(xmlPropVal), null);
                            break;
                        case "Int64":
                            prop.SetValue(entity, long.Parse(xmlPropVal), null);
                            break;
                        case "Double":
                            prop.SetValue(entity, double.Parse(xmlPropVal), null);
                            break;
                        case "RequestMsgType":
                        case "ResponseMsgType":
                        case "Event":
                            break;
                        case "List'1":// ResponseMessageNews type
                            var genericArguments = prop.PropertyType.GetGenericArguments();
                            if (genericArguments[0].Name == "Article")
                            {
                                List<Article> articles = new List<Article>();
                                foreach (var item in root.Element(propName).Elements("item"))
                                {
                                    var article = new Article();
                                    FillEntityWithXml(article, new XDocument(item));
                                    articles.Add(article);
                                }
                                prop.SetValue(entity, articles, null);
                            }
                            break;
                        case "Music":
                            Music music = new Music();
                            FillEntityWithXml(music, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, music, null);
                            break;
                        case "Image":
                            Image image = new Image();
                            FillEntityWithXml(image, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, image, null);
                            break;
                        case "Voice":
                            Voice voice = new Voice();
                            FillEntityWithXml(voice, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, voice, null);
                            break;
                        case "Video":
                            Video video = new Video();
                            FillEntityWithXml(video, new XDocument(root.Element(propName)));
                            prop.SetValue(entity, video, null);
                            break;
                        default:
                            prop.SetValue(entity, xmlPropVal, null);
                            break;
                    }

                }
            }

        }

        public  static XDocument ConvertEntityToXml<T>(this T entity) where T : class, new()
        {
            entity = entity ?? new T();
            var doc = new XDocument();
            doc.Add(new XElement("xml"));
            var root = doc.Root;
            var propNameOrder = new List<string>() { "FromUserName", "ToUserName", "CreateTime", "MsgType" };
            if(entity  is ResponseMessageNews)
            {
                propNameOrder.AddRange(new string[] { "ArticleCount","Articles","Title","Description","PicUrl","Url"});
            }
            else if( entity  is ResponseMessageMusic)
            {

                propNameOrder.AddRange(new string[] { "Music", "Title", "Description", "MusicUrl", "HQMusicUrl" });
            }
            else if(entity  is ResponseMessageVoice||entity is ResponseMessageImage)
            {
                propNameOrder.AddRange(new string[] { "Voice", "MediaId" });
            }
            else if(entity is ResponseMessageVideo)
            {

                propNameOrder.AddRange(new string[] { "Video", "MediaId", "Title", "Description" });
            }
            else
            {
                propNameOrder.AddRange(new string[] { "Content" });
            }
            Func<string, int> orderByPropName = propNameOrder.IndexOf;
            var props = entity.GetType().GetProperties().OrderBy(p => orderByPropName(p.Name)).ToList();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                if (propName == "Articles")
                {
                    var artcleElement = new XElement("Articles");
                    var articles = prop.GetValue(entity, null) as List<Article>;
                    foreach (var article in articles)
                    {
                        var subNodes = ConvertEntityToXml(article).Root.Elements();
                        artcleElement.Add(new XElement("item", subNodes));
                    }
                    root.Add(artcleElement);
                }
                else if (propName == "Music" || propName == "Image" || propName == "Video" || propName == "Voice")
                {
                    var musicElement = new XElement(propName);
                    var media = prop.GetValue(entity, null);
                    var subNodes =  ConvertEntityToXml(media).Root.Elements();
                    musicElement.Add(subNodes);
                    root.Add(musicElement);
                }
                else
                {
                    switch (prop.PropertyType.Name)
                    {
                        case "String":
                            root.Add(new XElement(propName, new XCData(prop.GetValue(entity, null) as string ?? "")));
                            break;
                        case "DateTime":
                            root.Add(new XElement(propName, DatetimeHelper.getWeixinDateTime((DateTime)prop.GetValue(entity,null))));
                            break;
                        case "ResponseMsgType":
                            root.Add(new XElement(propName, new XCData(prop.GetValue(entity, null).ToString().ToLower())));
                            break;
                        case "Articel":
                            root.Add(new XElement(propName, prop.GetValue(entity, null).ToString().ToLower()));
                            break;
                        default:
                            root.Add(new XElement(propName, prop.GetValue(entity, null)));
                            break;
                    }
                }
            }
            return doc;
        }

        public static  T CreateResponseMessage<T> (this IRequestMessageBase requestMessage) where T : ResponseMessageBase
        {

            try
            {
                var tType = typeof(T);
                var responseName = tType.Name.Replace("ResponseMessage", "");
                ResponseMsgType msgType = (ResponseMsgType)Enum.Parse(typeof(ResponseMsgType), responseName);
                return CreateFromRequestMessage(requestMessage, msgType) as  T;
            }
            catch (Exception ex)
            {

                throw new WeixinException(string.Format("CreateResponseMessage:{0}", ex));
            }
        }
        private  static ResponseMessageBase  CreateFromRequestMessage (IRequestMessageBase requestMessage,ResponseMsgType msgType)
        {
            ResponseMessageBase responseMessage = null;
            try
            {
                switch (msgType)
                {
                    case ResponseMsgType.Text:
                        responseMessage = new ResponseMessageText();
                        break;
                    case ResponseMsgType.News:
                        responseMessage = new ResponseMessageNews();
                        break;
                    case ResponseMsgType.Music:
                        responseMessage = new ResponseMessageMusic();
                        break;
                    case ResponseMsgType.Image:
                        responseMessage = new ResponseMessageImage();
                        break;
                    case ResponseMsgType.Voice:
                        responseMessage = new ResponseMessageVoice();
                        break;
                    case ResponseMsgType.Video:
                        responseMessage = new ResponseMessageVideo();
                        break;
                    default:
                        responseMessage = new ResponseMessageBase();
                        break;
                }
                responseMessage.FromUserName = requestMessage.ToUserName;
                responseMessage.ToUserName = requestMessage.FromUserName;
                responseMessage.CreateTime = DateTime.Now;
            }
            catch (Exception ex)
            {

                throw new WeixinException(string.Format("CreateResponseMessage:{0}", ex));
            }
            return responseMessage;
        }
      
    }
   
}
