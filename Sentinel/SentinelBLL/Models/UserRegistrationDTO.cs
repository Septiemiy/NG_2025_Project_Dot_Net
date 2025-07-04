using SentinelBLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SentinelBLL.Models
{
    public class UserRegistrationDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public UsersRoles Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
