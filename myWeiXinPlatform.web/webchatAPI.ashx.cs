using myWeiXinPlatform.common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace myWeiXinPlatform.web
{
    /// <summary>
    /// webchatAPI 的摘要说明
    /// </summary>
    public class webchatAPI : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string signature = context.Request["signature"];
            string nonce = context.Request["nonce"];
            string timestamp = context.Request["timestamp"];
            string echostr = context.Request["echostr"];
            if (context.Request.HttpMethod == "GET")
            {
                if (CheckSignature.Check(signature, timestamp, nonce))
                {
                    context.Response.Write(echostr);
                }
            }
            else
            {
                //if (!CheckSignature.Check(signature, timestamp, nonce))
                //{
                //    context.Response.Write("参数错误");
                //    context.Response.End();
                //}
                var maxRecordCount = 10;
                var messageHandler = new CustomMessageHandler(context.Request.InputStream, maxRecordCount);
                try
                {
                    messageHandler.RequestDocument.Save(context.Server.MapPath("~/" + DateTime.Now.Ticks + "-" + messageHandler.RequestMessage.MsgType + ".txt"));

                    messageHandler.Execute();
                  
                    messageHandler.ResponseDocument.Save(context.Server.MapPath("~/" + DateTime.Now.Ticks + "-" + messageHandler.ResponseMessage.ToUserName + ".txt"));

                    context.Response.Write(messageHandler.ResponseDocument.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf - 8\"?>", ""));

                }
                catch (Exception ex)
                {
                    using (TextWriter tw = new StreamWriter(context.Server.MapPath("~/" + DateTime.Now.Ticks + ".txt")))
                    {
                        tw.WriteLine(ex.Message);
                        tw.WriteLine(ex.InnerException.Message);
                        if (messageHandler.ResponseDocument != null)
                        {
                            tw.WriteLine(messageHandler.ResponseDocument.ToString());
                        }

                        tw.Flush();
                        tw.Close();
                    }

                }
                finally
                {
                    context.Response.End();
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}