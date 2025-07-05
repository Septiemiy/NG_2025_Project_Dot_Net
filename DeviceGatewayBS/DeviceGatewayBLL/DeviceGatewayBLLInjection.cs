using Azure.Messaging.ServiceBus;
using DeviceGatewayBLL.Profiles;
using DeviceGatewayBLL.Services;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceGatewayBLL
{
    public static class DeviceGatewayBLLInjection
    {
        public static void AddDeviceGatewayBLL(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ITriggerService, TriggerService>();
            services.AddScoped<IThresholdService, ThresholdService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<ITelemetryService, TelemetryService>();
            services.AddSingleton<ServiceBusClient>(provider =>
            {
                var connectionString = configuration.GetConnectionString("ServiceBusConnectionString");
                return new ServiceBusClient(connectionString);
            });

            services.AddAutoMapper(typeof(TriggerMapperProfile));
            services.AddAutoMapper(typeof(ThresholdMapperProfile));
            services.AddAutoMapper(typeof(CommandMapperProfile));
            services.AddAutoMapper(typeof(TelemetryMapperProfile));
        }
    }
}
