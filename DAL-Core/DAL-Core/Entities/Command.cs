using DAL_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Core.Entities
{
    public class Command : BaseEntity
    {
        public Guid DeviceId { get; set; }
        public string CommandName { get; set; }
        public DateTime Timestamp { get; set; }
        public CommandStatus Status { get; set; }
        public UsersRoles Role { get; set; }
    }
}
