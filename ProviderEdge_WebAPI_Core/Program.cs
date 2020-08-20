using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.IO;

namespace ProviderEdge_WebAPI_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public static void Main(string[] args)
        //{
        //    // ASP.NET Core 3.0+:
        //    // The UseServiceProviderFactory call attaches the
        //    // Autofac provider to the generic hosting mechanism.
        //    var host = Host.CreateDefaultBuilder(args)
        //        .UseServiceProviderFactory(new AutofacServiceProviderFactory())
        //        .ConfigureWebHostDefaults(webHostBuilder =>
        //        {
        //            webHostBuilder
        //      .UseContentRoot(Directory.GetCurrentDirectory())
        //      .UseIISIntegration()
        //      .UseStartup<Startup>();
        //        })
        //        .Build();

        //    host.Run();
        //}

    }
}
