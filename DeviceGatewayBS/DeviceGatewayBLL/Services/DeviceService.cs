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
            _httpClient = httpClientFactory.CreateClient("RegisterDeviceClient");
        }

        public async Task<HttpResponseMessage> RegisterDeviceAsync(DeviceDTO deviceDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("device/register", deviceDTO);
            
            return response;
        }
    }
}
