using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Service.Interface
{
    public interface IUserService
    {
        Task<string> CreateUserAsync(UserRegisterDTO userRegisterDTO);
        Task<string> LoginUserAsync(UserLoginDTO userLoginDTO);
    }
}
