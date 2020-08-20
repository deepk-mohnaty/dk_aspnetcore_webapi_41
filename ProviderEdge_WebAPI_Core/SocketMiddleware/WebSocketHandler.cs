﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using ProviderEdge_V3_Core.Common.CommonEntities;

namespace ProviderEdge_WebAPI_Core.SocketMiddleware
{
    public abstract class WebSocketHandler
    {
        protected ConnectionManager objWebSocketConnectionManager { get; set; }
        protected MessageQueueManager objMQManager { get; set; }


        public WebSocketHandler(ConnectionManager objConnectionManager)
        {
            objWebSocketConnectionManager = objConnectionManager;
            objMQManager = new MessageQueueManager();
        }
        public virtual void OnConnected(WebSocket objWebSocket, string socketId)
        {
            objWebSocketConnectionManager.AddSocket(objWebSocket, socketId);
        }
        public virtual async Task OnDisconnectedAsync(WebSocket objWebSocket, UserOnlineContext objUserOnlineContext)
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

        internal Task OnDisconnectedAsync(WebSocket socket)
        {
            throw new NotImplementedException();
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(objWebSocketConnectionManager.GetSocketById(socketId), message);
        }

        public async Task SendMessageToAllSync(string message)
        {
            foreach(var pair in objWebSocketConnectionManager.GetAll())
            {
               if(pair.Value.State == WebSocketState.Open)
                {
                  await  SendMessageAsync(pair.Value, message);
                }
            }
        }

        public void GetMessageFromMQ()
        {
            if (objMQManager.IsItemPresent())
            {
                MessageQueueSocketModel objMQSocketModel;
                objMQSocketModel = objMQManager.DequeueMessage();
                if (objMQSocketModel != null)
                {
                  string userLoginId=  objMQSocketModel.objUserOnlineContext.UserLoginId;
                  string message = objMQSocketModel.Message;

                 
                   
                }
            }
        }

        public abstract Task ReceiveAsync(WebSocket objWebSocket, WebSocketReceiveResult result, byte[] buffer, UserOnlineContext objUserOnlineContext);

    }
}
