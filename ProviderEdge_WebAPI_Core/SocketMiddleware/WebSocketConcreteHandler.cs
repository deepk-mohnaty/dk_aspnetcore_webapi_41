using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using ProviderEdge_V3_Core.Common.CommonEntities;

namespace ProviderEdge_WebAPI_Core.SocketMiddleware
{
    public class WebSocketConcreteHandler:WebSocketHandler
    {
        public WebSocketConcreteHandler(ConnectionManager objConnectionManager)
            : base(objConnectionManager)
        {

        }
        public override async Task ReceiveAsync(WebSocket objWebSocket, WebSocketReceiveResult result, byte[] buffer, UserOnlineContext objUserOnlineContext)
        {
            var socketId = objWebSocketConnectionManager.GetSocketId(objWebSocket);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

            Debug.Write("message received from: " + socketId + ", Message: " + message);

            MessageQueueSocketModel objMQSocketModel = new MessageQueueSocketModel()
            {
                SocketResult = result,
                Message = message,
                objWebSocket = objWebSocket,
                objUserOnlineContext= objUserOnlineContext
            };
            objMQManager.EnqueueMessage(objMQSocketModel);
        }
    }
}
