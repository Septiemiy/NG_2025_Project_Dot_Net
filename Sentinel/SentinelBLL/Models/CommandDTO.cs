using SentinelBLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Models
{
    public class CommandDTO
    {
        public Guid DeviceId { get; set; }
        public string CommandName { get; set; }
        public DateTime Timestamp { get; set; }
        public CommandStatus Status { get; set; }
        public UsersRoles Role { get; set; }
    }
}
