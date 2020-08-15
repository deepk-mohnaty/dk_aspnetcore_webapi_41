using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace ProviderEdge_WebAPI_Core.SocketMiddleware
{
    public abstract class WebSocketHandler
    {
        protected ConnectionManager objWebSocketConnectionManager { get; set; }
        public WebSocketHandler(ConnectionManager objConnectionManager)
        {
            objWebSocketConnectionManager = objConnectionManager;
        }
        public virtual void OnConnected(WebSocket objWebSocket)
        {
            objWebSocketConnectionManager.AddSocket(objWebSocket);
        }
        public virtual async Task OnDisconnectedAsync(WebSocket objWebSocket)
        {
            await objWebSocketConnectionManager.RemoveSocket(objWebSocketConnectionManager.GetSocketId(objWebSocket));
        }
        public async Task SendMessageAsync(WebSocket objWebSocket, string message)
        {
            if (objWebSocket.State != WebSocketState.Open)
                return;

             await objWebSocket.SendAsync(
                   buffer: new ArraySegment<byte> (array: Encoding.ASCII.GetBytes(message),
                                                   offset: 0,
                                                   count: message.Length),
                               messageType: WebSocketMessageType.Text,
                               endOfMessage: true,
                               cancellationToken : CancellationToken.None);


        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(objWebSocketConnectionManager.GetSocketById(socketId), message);
        }

        public async Task SendMEssageToAllSync(string message)
        {
            foreach(var pair in objWebSocketConnectionManager.GetAll())
            {
               if(pair.Value.State == WebSocketState.Open)
                {
                  await  SendMessageAsync(pair.Value, message);
                }
            }
        }

        public abstract Task ReceiveAsync(WebSocket objWebSocket, WebSocketReceiveResult result, byte[] buffer);

    }
}
