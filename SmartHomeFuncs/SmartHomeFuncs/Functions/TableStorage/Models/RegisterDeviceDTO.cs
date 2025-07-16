using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeFuncs.Functions.TableStorage.Models
{
    public class RegisterDeviceDTO
    {
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public string CategoryId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
