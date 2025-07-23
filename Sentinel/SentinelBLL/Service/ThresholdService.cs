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
    public class ThresholdService : IThresholdService
    {
        private readonly IThresholdClient _thresholdClient;
        
        public ThresholdService(IThresholdClient thresholdClient)
        {
            _thresholdClient = thresholdClient;
        }
        
        public async Task<ThresholdDTO> CreateThresholdAsync(ThresholdDTO thresholdDTO)
        {
            try
            {
                return await _thresholdClient.CreateThresholdAsync(thresholdDTO);
            }
            catch(ApiException ex)
            {
                return null;
            }
        }
    }
}
