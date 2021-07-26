using FluentValidation.AspNetCore;
using InHouse.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InHouse.API.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //Serilog Logging
            services.AddLogging();

            //Enable CORS
            services.AddCors(option =>
            {
                option.AddPolicy(
                    "InHouseCorsPolicy",
                    builder =>
                        builder
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin());
            });

            // Add default Filters to all Controller more generic way
            services.AddControllers(options =>
            {
                options.Filters.Add(new ApiExceptionFilterAttribute());
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ProblemDetails), 500));

            }).AddFluentValidation(fv => fv.ImplicitlyValidateChildProperties = true)
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
                opts.JsonSerializerOptions.IgnoreNullValues = true;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // access HTTPContext Accessor
            services.AddHttpContextAccessor();
        }
    }
}
