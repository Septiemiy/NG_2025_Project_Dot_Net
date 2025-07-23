using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeFuncs.Functions.Command.Models
{
    public class CommandDTO
    {
        public string DeviceId { get; set; }
        public string CommandName { get; set; }
        public string? Value { get; set; }
    }
}
