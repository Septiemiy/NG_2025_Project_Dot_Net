using DeviceGatewayBLL.Profiles;
using DeviceGatewayBLL.Services;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DeviceGatewayBLL
{
    public static class DeviceGatewayBLLInjection
    {
        public static void AddDeviceGatewayBLL(
            this IServiceCollection services)
        {
            services.AddScoped<ITriggerService, TriggerService>();
            services.AddScoped<IThresholdService, ThresholdService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<ITelemetryService, TelemetryService>();

            services.AddAutoMapper(typeof(TriggerMapperProfile));
            services.AddAutoMapper(typeof(ThresholdMapperProfile));
            services.AddAutoMapper(typeof(CommandMapperProfile));
            services.AddAutoMapper(typeof(TelemetryMapperProfile));
        }
    }
}
