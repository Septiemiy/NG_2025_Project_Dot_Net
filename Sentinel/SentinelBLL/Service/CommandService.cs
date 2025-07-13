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
    public class CommandService : ICommandService
    {
        private readonly ICommandClient _commandClient;

        public CommandService(ICommandClient commandClient)
        {
            _commandClient = commandClient;
        }

        public async Task<CommandDTO> CreateCommandAsync(CommandDTO commandDTO)
        {
            return await _commandClient.CreateCommandAsync(commandDTO);
        }
    }
}
