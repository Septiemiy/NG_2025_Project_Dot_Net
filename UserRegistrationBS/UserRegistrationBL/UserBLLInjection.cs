using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationBL.Profiles;
using UserRegistrationBL.Services;
using UserRegistrationBL.Services.Interfaces;

namespace UserRegistrationBL
{
    public static class UserBLLInjection
    {
        public static void AddUserBLL(
            this IServiceCollection services)
        {   
            services.AddScoped<IUserService, UserService>();
            
            services.AddAutoMapper(typeof(UserMapperProfile));
        }
    }
}
