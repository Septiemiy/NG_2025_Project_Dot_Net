using Refit;
using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Clients
{
    public interface ITelemetryClient
    {
        [Post("/api/telemetry/get-data")]
        Task<Guid> SaveTelemetryDataAsync([Body] TelemetryDTO telemetryDTO);
    }
}
