using Refit;
using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Clients
{
    public interface IUserClient
    {
        [Post("/api/user/register")]
        Task<Guid> CreateUserAsync(UserDTO userDTO);
    }
}
