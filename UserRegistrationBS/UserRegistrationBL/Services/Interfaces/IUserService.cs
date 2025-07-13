using DAL_Core.Entities;
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
        Task<RegisterLoginResultDTO> CreateUserAsync(UserRegisterDTO userDto);
        Task<RegisterLoginResultDTO> CheckUserLoginAsync(UserLoginDTO userLoginDto);
    }
}
