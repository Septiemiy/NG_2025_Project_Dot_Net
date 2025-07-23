using Refit;
using SentinelBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentinelBLL.Clients
{
    public interface ICommandClient
    {
        [Post("/api/command/sendCommand")]
        Task<CommandDTO> SendCommandAsync([Body] CommandDTO commandDTO);
    }
}
