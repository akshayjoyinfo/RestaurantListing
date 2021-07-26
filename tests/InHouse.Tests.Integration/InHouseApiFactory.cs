using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace InHouse.Tests.Integration
{
    public class InHouseApiFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.integration.test.json").Build();
            
            builder.ConfigureServices(services =>
            {
                services.AddMvc(opt =>
                {
                    //add a global anonymous filter
                    opt.Filters.Add(new AllowAnonymousFilter());

                    //add a filter for adding a fake claimsprincipal so that the user service
                    //correctly identifies the user
                });

                
                
                // Build the service provider.
                var sp = services.BuildServiceProvider();

            });
        }
    }
}
