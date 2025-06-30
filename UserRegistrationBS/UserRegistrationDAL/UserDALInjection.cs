using DAL_Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationDAL.Repositories;
using UserRegistrationDAL.Repositories.Interface;

namespace UserRegistrationDAL
{
    public static class UserDALInjection
    {
        public static void AddUserDAL(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SmartHomeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
