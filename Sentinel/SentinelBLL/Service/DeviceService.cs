using Refit;
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

        public async Task<RegisterDeviceResultDTO> RegisterDeviceAsync(DeviceDTO deviceDTO)
        {
            if (deviceDTO == null)
            {
                return new RegisterDeviceResultDTO
                {
                    IsSuccess = false,
                    Message = "Device data cannot be null"
                };
            }

            try
            {
                var result = await _deviceClient.RegisterDeviceAsync(deviceDTO);

                return result;
            }
            catch (ApiException ex)
            {
                var error = await ex.GetContentAsAsync<RegisterDeviceResultDTO>();

                return error;
            }
        }

        public async Task<ICollection<DeviceDTO>> GetAllDevicesAsync()
        {
            try
            {
                return await _deviceClient.GetAllDevicesAsync();
            }
            catch (ApiException ex)
            {
                return null;
            }
        }

        public async Task<DeviceDTO> GetDeviceByIdAsync(Guid deviceId)
        {
            try
            {
                return await _deviceClient.GetDeviceByIdAsync(deviceId);
            }
            catch (ApiException ex)
            {
                return null;
            }
        }
    }
}
