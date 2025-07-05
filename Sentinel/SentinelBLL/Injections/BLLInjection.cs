using Microsoft.Extensions.DependencyInjection;
using SentinelBLL.Service;
using SentinelBLL.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Injections
{
    public static class BLLInjection
    {
        public static void AddSentinelServices(
            this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITelemetryService, TelemetryService>();
        }
    }
}
