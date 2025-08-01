﻿using DeviceGatewayBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<HttpResponseMessage> RegisterDeviceAsync(DeviceDTO deviceDTO);
        Task<HttpResponseMessage> GetDevicesAsync();
        Task<HttpResponseMessage> GetDeviceByIdAsync(Guid deviceId);
    }
}
