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
                    .Get<UserClientSettings>() ?? throw new ArgumentNullException("UserClient configuration is missing");

            Console.WriteLine($"UserClient BaseAddress: {userSettings.BaseAddress}");

            services.AddRefitClient<IUserClient>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(userSettings.BaseAddress));
        }
    }
}
