using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProviderEdge_V3_Core.GblService.Service;
using ProviderEdge_V3_Core.GblService.InterFace;
using ProviderEdge_V3_Core.GblRepository.Interface;
using ProviderEdge_V3_Core.GblRepository.Repository;
using ProviderEdge_WebAPI_Core.Middleware;
using ProviderEdge_V3_Core.Common.CommonEntities;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ProviderEdge_WebAPI_Core.HostedServices;
using ProviderEdge_WebAPI_Core.SocketMiddleware;

namespace ProviderEdge_WebAPI_Core
{
    public class Startup
    {
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public Startup(IWebHostEnvironment env)
        {
            // In ASP.NET Core 3.0 `env` will be an IWebHostEnvironment, not IHostingEnvironment.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        //public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllers();
        //    services.Configure<ApplicationSettings>(Configuration.GetSection("AppSettings"));
        //    services.AddScoped<IGblSecurityRepository, GblSecurityRepository>();
        //    services.AddScoped<ISecurityService, SecurityService>();
        //    services.AddScoped<IHomeService, HomeService>();
        //    //services.AddHostedService<>

        //}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ApplicationSettings>(Configuration.GetSection("AppSettings"));

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            //services.AddSingleton<MessageQueueManager>();
            services.AddWebSocketManager();
            services.AddHostedService<DkHostedServiceChat>();

            services.AddScoped<IGblSecurityRepository, GblSecurityRepository>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IHomeService, HomeService>();
            

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,  
            // be sure to keep a reference to it as a property or field.
            //var builder = new ContainerBuilder();


            //return new AutofacServiceProvider(container);

        }
        
        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    // Register your own things directly with Autofac, like:
        //    //builder.RegisterModule(new MyApplicationModule());

        //    builder.RegisterType<HomeService>().As<IHomeService>();
        //    builder.RegisterType<GblSecurityRepository>().As<IGblSecurityRepository>();
        //    builder.RegisterType<SecurityService>().As<ISecurityService>();
            
        //}


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var objServiceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var objServiceProvider = objServiceScopeFactory.CreateScope().ServiceProvider;


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Use(async (context, next) =>
            {
                string strUrl = context.Request.Path;
                await next.Invoke();
            });

            app.UseCors(options => { options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });

            app.Use(async (context, next) =>
            {
                string strUrl = context.Request.Path;
                await next.Invoke();
            });

            app.UseMiddleware<DkJWTMiddlware>();

            app.UseWebSockets();

            //app.MapWhen(x=> x.)

            //app.UseAuthorization();

            app.MapWebSocketManager("/chat", objServiceProvider.GetService<WebSocketHandler>()); ;

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
