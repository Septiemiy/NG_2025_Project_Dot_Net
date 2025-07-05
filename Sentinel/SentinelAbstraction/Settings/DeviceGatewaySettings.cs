using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelAbstraction.Settings
{
    public class DeviceGatewaySettings
    {
        public const string SectionName = "RefitClients:DeviceGatewayClient";

        public string BaseAddress { get; set; } = string.Empty;
    }
}
