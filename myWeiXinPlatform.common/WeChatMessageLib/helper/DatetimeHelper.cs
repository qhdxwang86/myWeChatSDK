using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
   public class DatetimeHelper
    {
        public static DateTime BaseTime = new DateTime(1970, 1, 1);
        public static DateTime GetDateTimeFromXml(string datetimeFromXml)
        {
            return GetDateTimeFromXml(long.Parse(datetimeFromXml));
        }
        public static DateTime GetDateTimeFromXml(long datetimeFromXml)
        {
            return BaseTime.AddTicks((datetimeFromXml + 8 * 60 * 60) * 10000000);
        }
        public static  long   getWeixinDateTime(DateTime datetime)
        {
            return (datetime.Ticks - BaseTime.Ticks) / 1000000 - 8 * 60 * 60;
        }
    }
}
