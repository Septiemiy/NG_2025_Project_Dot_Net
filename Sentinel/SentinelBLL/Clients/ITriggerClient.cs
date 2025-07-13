using Refit;
using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Clients
{
    public interface ITriggerClient
    {
        [Post("api/trigger/createTrigger")]
        Task<TriggerDTO> CreateTriggerAsync([Body] TriggerDTO triggerDTO);
    }
}
