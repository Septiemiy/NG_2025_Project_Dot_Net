using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Service.Interface
{
    public interface IThresholdService
    {
        Task<ThresholdDTO> CreateThresholdAsync(ThresholdDTO thresholdDTO);
    }
}
