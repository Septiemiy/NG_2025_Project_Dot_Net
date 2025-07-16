
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
    public class TelemetryService : ITelemetryService
    {
        private readonly  ITelemetryClient _telemetryClient;

        public TelemetryService(ITelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public async Task<Guid> SaveTelemetryDataAsync(TelemetryDTO telemetryDTO)
        {
            return await _telemetryClient.SaveTelemetryDataAsync(telemetryDTO) ;
        }
    }
}
