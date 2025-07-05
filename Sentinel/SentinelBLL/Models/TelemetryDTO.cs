using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Models
{
    public class TelemetryDTO
    {
        public Guid DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public string DataType { get; set; }
        public string DataValue { get; set; }
    }
}
