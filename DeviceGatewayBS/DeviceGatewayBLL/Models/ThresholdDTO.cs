using DAL_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Models
{
    public class ThresholdDTO
    {
        public Guid DeviceId { get; set; }
        public string ThresholdCondition { get; set; }
        public string Value { get; set; }
        public DateTime? Timestamp { get; set; } = DateTime.Now;
        public ConditionsStatus? Status { get; set; }
    }
}
