using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class RequestMessageEvent_Location:RequestMessageEventBase
    {
        public override Event Event
        {
            get
            {
                return Event.LOCATION;
            }
        }
        //地理位置 纬度
        public double Latitude { get; set; }
        //地理位置 经度
        public double Longitude { get; set;}
        //地理位置  精度
        public double Precision { get; set; }
    }
}
