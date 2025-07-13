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
    public class TriggerService : ITriggerService
    {
        private readonly ITriggerClient _triggerClient;

        public TriggerService(ITriggerClient triggerClient)
        {
            _triggerClient = triggerClient;
        }

        public async Task<TriggerDTO> CreateTriggerAsync(TriggerDTO triggerDTO)
        {
            return await _triggerClient.CreateTriggerAsync(triggerDTO);
        }
    }
}
