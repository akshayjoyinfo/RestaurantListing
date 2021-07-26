using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InHouse.API.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
