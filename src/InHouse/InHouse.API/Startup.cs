using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InHouse.API.Installers;
using InHouse.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace InHouse.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);
            services.AddApplication(Configuration);
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddSerilog();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSwagger(option => { option.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("v1/swagger.json", "InHouse Restaurant Listing API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
