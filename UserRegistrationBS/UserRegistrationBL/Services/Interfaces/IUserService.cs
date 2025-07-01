using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRegistrationBL.Models;

namespace UserRegistrationBL.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(UserRegisterDTO userDto);
        Task<string> CheckUserLogin(UserLoginDTO userLoginDto);
    }
}
