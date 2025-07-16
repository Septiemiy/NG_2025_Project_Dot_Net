using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Models
{
    public class RegisterDeviceResultDTO
    {
        public bool IsSuccess { get; set; }
        public Guid? DeviceId { get; set; }
        public string? Message { get; set; }
    }
}
