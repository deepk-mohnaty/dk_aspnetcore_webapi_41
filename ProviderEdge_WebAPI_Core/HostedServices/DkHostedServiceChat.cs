using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using ProviderEdge_WebAPI_Core.SocketMiddleware;

namespace ProviderEdge_WebAPI_Core.HostedServices
{
    public class DkHostedServiceChat: IHostedService
    {
        private Timer _objTimer;
        private bool _isTaskRunning;
        private WebSocketHandler _objWebSocketHandler;

        public DkHostedServiceChat(WebSocketHandler objWebSocketHandler)
        {
            _objWebSocketHandler = objWebSocketHandler;
        }

        public Task StartAsync(CancellationToken objCancellationToken)
        {
            _isTaskRunning = false;
            _objTimer = new Timer(TimeOutEvent, null, 0, 5000);
            return Task.CompletedTask;
        }

        private void TimeOutEvent(object state)
        {
            try
            {
                if (!_isTaskRunning)
                {
                    _isTaskRunning = true;
                    _objTimer.Change(9999000, Timeout.Infinite);
                    Debug.WriteLine("Hi Deepak, Hosted servce got executed...");

                    ListenOnMQ();

                    _isTaskRunning = false;
                    _objTimer.Change(5000, 5000);
                }
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                _isTaskRunning = false;
                _objTimer.Change(5000, 5000);
            }
        }

        private async Task ListenOnMQ()
        {
            int counter = 0;
            do
            {
               await _objWebSocketHandler.GetMessageFromMQ();
            }
            while (counter++ < 5);
        }

        public Task StopAsync(CancellationToken objCancellationToken)
        {
            _objTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }

}
