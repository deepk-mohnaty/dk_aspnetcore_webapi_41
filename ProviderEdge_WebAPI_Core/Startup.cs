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

namespace ProviderEdge_WebAPI_Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<ApplicationSettings>(Configuration.GetSection("AppSettings"));
            services.AddScoped<IGblSecurityRepository, GblSecurityRepository>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IHomeService, HomeService>();
            //services.AddHostedService<>

        }

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    services.AddControllers();
        //    services.Configure<ApplicationSettings>(Configuration.GetSection("AppSettings"));
        //    //services.AddScoped<IGblSecurityRepository, GblSecurityRepository>();
        //    //services.AddScoped<ISecurityService, SecurityService>();
        //    //services.AddScoped<IHomeService, HomeService>();
        //    //services.AddHostedService<>

        //    // Register dependencies, populate the services from
        //    // the collection, and build the container. If you want
        //    // to dispose of the container at the end of the app,  
        //    // be sure to keep a reference to it as a property or field.
        //    var builder = new ContainerBuilder();

        //    builder.RegisterType<HomeService>().As<IHomeService>();
        //    builder.RegisterType<GblSecurityRepository>().As<IGblSecurityRepository>();
        //    builder.RegisterType<SecurityService>().As<ISecurityService>();

        //    builder.Populate(services);
        //    var container = builder.Build();

        //    return new AutofacServiceProvider(container);

        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<DkJWTMiddlware>();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
