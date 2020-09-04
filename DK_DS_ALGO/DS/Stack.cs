using System;
using System.Collections.Generic;
using System.Text;

namespace DK_DS_ALGO.DS
{
   public class Stack
    {
        private StackItem _objHead;
        private object _objLock;

        public void PushItem(string value)
        {
            StackItem objNew = new StackItem();
            objNew.ItemValue = value;

            lock (_objLock)
            {
                if (_objHead != null)
                {
                    objNew.ObjNext = _objHead;
                }
                _objHead = objNew;
            }
            
        }

        public string PopItem()
        {
            string value = null;

            lock (_objLock)
            {
                if (_objHead != null)
                {
                    value = _objHead.ItemValue;
                    _objHead = _objHead.ObjNext;
                }
            }

            return value;
        }
    }



   public class StackItem
    {
        public string ItemValue { get; set; }
        public StackItem ObjNext { get; set; }
    }
}
