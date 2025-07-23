using DAL_Core;
using DeviceGatewayDAL.Repositories;
using DeviceGatewayDAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayDAL
{
    public static class DeviceGatewayDALInjection
    {
        public static void AddDeviceGatewayDAL(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SmartHomeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IThresholdRepository, ThresholdRepository>();
            services.AddScoped<ICommandRepository, CommandRepository>();
            services.AddScoped<ITelemetryRepository, TelemetryRepository>();
        }
    }
}
