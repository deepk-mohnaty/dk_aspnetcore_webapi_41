using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ProviderEdge_WebAPI_Core.HostedServices
{
    public class DkHostedServiceChat: IHostedService
    {
        private Timer _objTimer;

        public Task StartAsync(CancellationToken objCancellationToken)
        {
            _objTimer = new Timer(HelloWorld, null, 0, 10000);
            return Task.CompletedTask;
        }

        private void HelloWorld(object state)
        {
            Debug.WriteLine("Hi Deepak, Hosted servce got executed...");
        }

        public Task StopAsync(CancellationToken objCancellationToken)
        {
            _objTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }

}
