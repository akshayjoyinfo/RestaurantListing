using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InHouse.API.Installers
{
    public static class InstallerExtensions
    {
        /// <summary>
        /// The InstallServicesInAssembly
        /// </summary>
        /// <param name="services">The services<see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var apiInstallers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            apiInstallers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
