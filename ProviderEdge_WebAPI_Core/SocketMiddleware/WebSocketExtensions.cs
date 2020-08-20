using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ProviderEdge_WebAPI_Core.SocketMiddleware
{
    public static class WebSocketExtensions
    {
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder App, PathString path, WebSocketHandler handler)
        {
            // return App.Map(path, (_app) => _app.UseMiddleware<WebSocketManagerMiddleware>(handler));
            return App.Map(path, (_app) => _app.UseMiddleware<DKWebSocketManagerMiddleware>(handler));
        }

        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            services.AddTransient<ConnectionManager>();
            services.AddTransient<MessageQueueManager>();
            services.AddSingleton<WebSocketHandler,WebSocketConcreteHandler>();

            //foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            //{
            //    if(type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
            //    {
            //        services.AddSingleton(type);
            //    }
            //}

            return services;
        }




    }
}
