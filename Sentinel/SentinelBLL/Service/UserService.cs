using Refit;
using SentinelBLL.Clients;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Service
{
    public class UserService : IUserService
    {
        private readonly IUserClient _userClient;

        public UserService(IUserClient userClient)
        {
            _userClient = userClient;
        }

        public async Task<RegisterLoginResultDTO> CreateUserAsync(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var response = await _userClient.CreateUserAsync(userRegisterDTO);

                return response;
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<RegisterLoginResultDTO>();

                return error;
            }
        }

        public async Task<string> LoginUserAsync(UserLoginDTO userLoginDTO)
        {
            return await _userClient.LoginUserAsync(userLoginDTO);
        }
    }
}
