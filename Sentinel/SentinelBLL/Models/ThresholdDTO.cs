using SentinelBLL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Models
{
    public class ThresholdDTO
    {
        public Guid DeviceId { get; set; }
        public string ThresholdName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Condition { get; set; }
        public string Action { get; set; }
        public ConditionsStatus Status { get; set; }
    }
}
