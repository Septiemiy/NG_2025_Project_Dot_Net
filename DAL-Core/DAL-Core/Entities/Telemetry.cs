using DAL_Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Core.Entities
{
    public class Telemetry : BaseEntity
    {
        public Guid DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public DataTypes DataType { get; set; }
        public string DataValue { get; set; }
    }
}
