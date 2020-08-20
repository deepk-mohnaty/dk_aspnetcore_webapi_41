//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.WebSockets;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using ProviderEdge_V3_Core.Common.CommonEntities;

//namespace ProviderEdge_WebAPI_Core.SocketMiddleware
//{
//    public class WebSocketManagerMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private WebSocketHandler _objWebSocketHandler;

//        public WebSocketManagerMiddleware(RequestDelegate next, WebSocketHandler objWebSocketHandler)
//        {
//            _next = next;
//            _objWebSocketHandler = objWebSocketHandler;

//        }

//        public async Task Invoke(HttpContext context)
//        {
//            if(!context.WebSockets.IsWebSocketRequest)
//            {
//                return;
//            }

//            UserOnlineContext objUserOnlineContext =(UserOnlineContext) context.Items["ONLINEOBJ"];

//            var socket =await context.WebSockets.AcceptWebSocketAsync();
//             _objWebSocketHandler.OnConnected(socket, objUserOnlineContext.UserLoginId);

//            await Receive(socket, async (result, buffer) =>
//            {
//                if (result.MessageType == WebSocketMessageType.Text)
//                {
//                    await _objWebSocketHandler.ReceiveAsync(socket, result, buffer,null);
//                    return;
//                }
//                else if(result.MessageType == WebSocketMessageType.Close)
//                {
//                    await _objWebSocketHandler.OnDisconnectedAsync(socket,null);
//                    return;
//                }

//            });
//        }

//        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
//        {
//            var buffer = new byte[1024*4];

//            while(socket.State== WebSocketState.Open)
//            {
//                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);

//                if (handleMessage != null)
//                {
//                    handleMessage(result, buffer);
//                }
//            }
//        }




//    }

//}
