using DeviceGatewayBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Services.Interfaces
{
    public interface IThresholdService
    {
        Task<Guid> AddThresholdAsync(ThresholdDTO thresholdDTO);
        Task<bool> isThresholdExists(ThresholdDTO thresholdDTO);
        Task<bool> SendThresholdAsync(ThresholdDTO thresholdDTO);
    }
}
