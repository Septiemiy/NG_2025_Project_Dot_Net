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

        public async Task<Guid> CreateUserAsync(UserDTO userDTO)
        {
            return await _userClient.CreateUserAsync(userDTO);
        }
    }
}
