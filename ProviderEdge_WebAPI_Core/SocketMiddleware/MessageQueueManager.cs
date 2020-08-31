using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ProviderEdge_V3_Core.Common.CommonEntities;

namespace ProviderEdge_WebAPI_Core.SocketMiddleware
{
    public class MessageQueueManager
    {
        private ConcurrentQueue<MessageQueueSocketModel> _objConcurrentQueue;

        private int _MQId;
        public MessageQueueManager()
        {
            _objConcurrentQueue = new ConcurrentQueue<MessageQueueSocketModel>();
            _MQId = 0;
        }
        public void EnqueueMessage (MessageQueueSocketModel objMQModel)
        {
            objMQModel.MQId = ++_MQId;
            _objConcurrentQueue.Enqueue(objMQModel);
        }
        public bool IsItemPresent()
        {
            return !_objConcurrentQueue.IsEmpty;
        }
        public MessageQueueSocketModel DequeueMessage()
        {
            MessageQueueSocketModel objMessageQueueModel = null;
            _objConcurrentQueue.TryDequeue(out objMessageQueueModel);
            return objMessageQueueModel;
        }
    }


    public class MessageQueueModel
    {
        public int MQId { get; set; }
        public string SenderLoginId { get; set; }
        public string[] ReceiverIds { get; set; }
        public string Message { get; set; }
        public MQOpType MQMessageType { get; set; }

    }

    public class MessageQueueSocketModel
    {
        public int MQId { get; set; }

        public UserOnlineContext objUserOnlineContext { get; set; }
        public WebSocketReceiveResult SocketResult { get; set; }
        public string Message { get; set; }
        public WebSocket objWebSocket { get; set; }

    }

    public class IncommingMessageModel
    {
        public string receiverid { get; set; }
        public string message { get; set; }
    }

    public class OutgoingMessageModel
    {
        public string senderid { get; set; }
        public string message { get; set; }
    }
    public enum MQOpType
    {
        OPEN,
        CLOSE,
        MESSAGE
    }
}
