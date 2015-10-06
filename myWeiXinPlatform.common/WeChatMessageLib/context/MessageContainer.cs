using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myWeiXinPlatform.common
{
    public class MessageContainer<T> : List<T>
    {
        public  int MaxRecordCount { get; set; }

        private MessageContainer()
        {

        }

        public  MessageContainer(int maxRecordCount)
        {
            MaxRecordCount = maxRecordCount;
        }
        new public  void  Add(T item)
        {
            base.Add(item);

            RemoveExrepssItems();
        }
        new public void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            RemoveExrepssItems();

        }
        new public  void Insert(int index ,T item)
        {
            base.Insert(index, item);
            RemoveExrepssItems();
        }
        new public  void InsertRange(int index,IEnumerable<T> collection)
        {
            base.InsertRange(index,collection);
            RemoveExrepssItems();
        }
        private void RemoveExrepssItems() {
            if (MaxRecordCount > 0 && base.Count > MaxRecordCount)
            {
                base.RemoveRange(0, base.Count - MaxRecordCount);
            }
        }
    }
}
