using Refit;
using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Clients
{
    public interface IThresholdClient
    {
        [Post("/api/threshold/createThreshold")]
        Task<ThresholdDTO> CreateThresholdAsync([Body] ThresholdDTO thresholdDTO);
    }
}
