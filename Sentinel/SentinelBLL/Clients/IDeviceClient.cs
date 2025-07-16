using Refit;
using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Clients
{
    public interface IDeviceClient
    {
        [Post("/api/device/registerDevice")]
        Task<RegisterDeviceResultDTO> RegisterDeviceAsync([Body] DeviceDTO deviceDTO);

        [Get("/api/device/getAll")]
        Task<ICollection<DeviceDTO>> GetAllDevicesAsync();

        [Get("/api/device/get/{deviceId}")]
        Task<DeviceDTO> GetDeviceByIdAsync(Guid deviceId);
    }
}
