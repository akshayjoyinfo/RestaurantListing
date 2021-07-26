using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InHouse.API.Installers
{
    public class SwaggerInstaller : IInstaller
    {
       
        public void InstallServices(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "InHouse Resturant Listing API", Version = "v1" });

                x.ExampleFilters();

                x.EnableAnnotations();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerExamplesFromAssemblyOf<Startup>();
        }
    }
}
