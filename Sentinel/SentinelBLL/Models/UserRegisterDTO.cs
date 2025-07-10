using SentinelBLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SentinelBLL.Models
{
    public class UserRegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UsersRoles Role { get; set; }
    }
}
