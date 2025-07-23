using Microsoft.AspNetCore.Http;
using Refit;
using SentinelBLL.Clients;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<CommandDTO> SendCommandAsync(CommandDTO commandDTO)
        {
            try
            {
                return await _commandClient.SendCommandAsync(commandDTO);
            }
            catch(ApiException ex)
            {
                return null;
            }
        }
    }
}
