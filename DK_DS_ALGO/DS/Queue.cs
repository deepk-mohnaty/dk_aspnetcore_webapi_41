using System;
using System.Collections.Generic;
using System.Text;

namespace DK_DS_ALGO.DS
{
   public class Queue
    {
        private QueueItem _objHead;
        private QueueItem _objTail;
        private object _objLock;

        public void Enqueue(string value)
        {
            QueueItem objNew = new QueueItem();
            objNew.ItemValue = value;

            lock (_objLock)
            {
                if (_objTail == null)
                {
                    _objHead = _objTail = objNew;
                }
                else
                {
                    _objTail.ObjNext = objNew;
                    _objTail = objNew;
                }
            }
           
        }

        public string Dequeue()
        {
            string value = null;
            
            lock (_objLock)
            {
                if (_objHead != null)
                {
                    if (_objHead.ObjNext == null)
                    {
                        value = _objHead.ItemValue;
                         _objHead=_objTail=null;
                    }
                    else
                    {
                        value = _objHead.ItemValue;
                        _objHead = _objHead.ObjNext;
                    }

                }
            }

            return value;
        }
    }



    public class QueueItem
    {
        public string ItemValue { get; set; }
        public QueueItem ObjNext { get; set; }
    }
}

