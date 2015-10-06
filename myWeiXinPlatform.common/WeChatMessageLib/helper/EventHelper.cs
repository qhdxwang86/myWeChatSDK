using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace myWeiXinPlatform.common
{
   public class EventHelper
    {
        public  static  Event   GetEventType(XDocument doc)
        {

            return GetEventType(doc.Root.Element("Event").Value);
        }
        public static Event  GetEventType(string str)
        {
            return (Event)Enum.Parse(typeof(Event), str, true);
        }
    }
}
