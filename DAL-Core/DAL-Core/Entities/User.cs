using DAL_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public UsersRoles Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
