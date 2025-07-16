using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Service.Interface
{
    public interface IDeviceService
    {
        Task<RegisterDeviceResultDTO> RegisterDeviceAsync(DeviceDTO deviceDTO);
        Task<ICollection<DeviceDTO>> GetAllDevicesAsync();
        Task<DeviceDTO> GetDeviceByIdAsync(Guid deviceId);
    }
}
