using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProviderEdge_V3_Core.Common.CommonEntities;

namespace ProviderEdge_WebAPI_Core.SocketMiddleware
{
    public class DKWebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;
        private WebSocketHandler _objWebSocketHandler;
       

        public DKWebSocketManagerMiddleware(RequestDelegate next, WebSocketHandler objWebSocketHandler)
        {
            _next = next;
            _objWebSocketHandler = objWebSocketHandler;
           

        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                return;
            }

            UserOnlineContext objUserOnlineContext = (UserOnlineContext)context.Items["ONLINEOBJ"];

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            _objWebSocketHandler.OnConnected(socket, objUserOnlineContext.UserLoginId);

            await Receive(socket, objUserOnlineContext, async (result, buffer, userContext) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    await _objWebSocketHandler.ReceiveAsync(socket, result, buffer, objUserOnlineContext);
                    return;
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _objWebSocketHandler.OnDisconnectedAsync(socket, objUserOnlineContext);
                    return;
                }

            });
        }

        private async Task Receive(WebSocket socket, UserOnlineContext objUserOnlineContext, Action<WebSocketReceiveResult, byte[],UserOnlineContext> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);
                handleMessage(result, buffer, objUserOnlineContext);
               
            }
        }




    }

}

