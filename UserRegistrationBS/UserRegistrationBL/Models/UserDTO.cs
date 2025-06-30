using DAL_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistrationBL.Models
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public UsersRoles Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
