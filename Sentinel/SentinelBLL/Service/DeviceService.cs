using SentinelBLL.Clients;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceClient _deviceClient;

        public DeviceService(IDeviceClient deviceClient)
        {
            _deviceClient = deviceClient;
        }

        public async Task<DeviceDTO> RegisterDeviceAsync(DeviceDTO deviceDTO)
        {
            return await _deviceClient.RegisterDeviceAsync(deviceDTO);
        }
    }
}
