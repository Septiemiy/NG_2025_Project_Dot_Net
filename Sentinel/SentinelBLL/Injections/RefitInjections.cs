using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using SentinelAbstraction.Settings;
using SentinelBLL.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Injections
{
    public static class RefitInjections
    {
        public static void AddRefitInjections(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var userSettings = configuration.GetSection(UserClientSettings.SectionName)
                    .Get<UserClientSettings>();

            var deviceGatewaySettings = configuration.GetSection(DeviceGatewaySettings.SectionName)
                    .Get<DeviceGatewaySettings>();

            services.AddRefitClient<IUserClient>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(userSettings.BaseAddress));

            services.AddRefitClient<ITelemetryClient>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(deviceGatewaySettings.BaseAddress));

            services.AddRefitClient<ICommandClient>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(deviceGatewaySettings.BaseAddress));

            services.AddRefitClient<IDeviceClient>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(deviceGatewaySettings.BaseAddress));

            services.AddRefitClient<IThresholdClient>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(deviceGatewaySettings.BaseAddress));

            services.AddRefitClient<ICategoryClient>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(deviceGatewaySettings.BaseAddress));
        }
    }
}
