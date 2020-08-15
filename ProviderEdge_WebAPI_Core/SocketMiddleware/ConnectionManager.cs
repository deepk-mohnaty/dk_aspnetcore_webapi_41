using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace ProviderEdge_WebAPI_Core.SocketMiddleware
{
    public class ConnectionManager
    {

        private ConcurrentDictionary<string, WebSocket> _objSocketList = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketById(string SocketId)
        {
            return _objSocketList.FirstOrDefault(p => p.Key == SocketId).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _objSocketList;
        }

        public string GetSocketId(WebSocket objWebSocket)
        {
            return _objSocketList.FirstOrDefault(p => p.Value == objWebSocket).Key;
        }

        public void AddSocket(WebSocket objWebSocket)
        {
            _objSocketList.TryAdd(GenerateSocketId(), objWebSocket);
        }

        public async Task RemoveSocket(string socketId)
        {
            WebSocket objWebSocket;
            _objSocketList.TryRemove(socketId, out objWebSocket);

            await objWebSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure, statusDescription: "Closed by connection manager", cancellationToken: CancellationToken.None);
        }

        private string GenerateSocketId()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
