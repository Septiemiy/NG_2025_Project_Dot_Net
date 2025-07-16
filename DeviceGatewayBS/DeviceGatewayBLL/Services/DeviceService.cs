using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly HttpClient _httpClient;
        public DeviceService(IHttpClientFactory httpClientFactory) 
        {
            _httpClient = httpClientFactory.CreateClient("DevicesClient");
        }

        public async Task<HttpResponseMessage> GetDevicesAsync()
        {
            return await _httpClient.GetAsync("device/getAll");
        }

        public async Task<HttpResponseMessage> RegisterDeviceAsync(DeviceDTO deviceDTO)
        {
            return await _httpClient.PostAsJsonAsync("device/register", deviceDTO);
        }

        public async Task<HttpResponseMessage> GetDeviceByIdAsync(Guid deviceId)
        {
            return await _httpClient.GetAsync($"device/get/{deviceId}");
        }
    }
}
