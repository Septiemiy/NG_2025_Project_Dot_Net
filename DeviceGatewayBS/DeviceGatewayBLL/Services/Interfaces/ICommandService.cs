using DeviceGatewayBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Services.Interfaces
{
    public interface ICommandService
    {
        Task<Guid> AddCommandAsync(CommandDTO commandDTO);
        Task<bool> SendCommandAsync(CommandDTO commandDTO);
    }
}
