using AutoMapper;
using Azure.Messaging.ServiceBus;
using DAL_Core.Entities;
using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using DeviceGatewayDAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DeviceGatewayBLL.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;
        private readonly ServiceBusSender _serviceBusSender;
        private const string TopicName = "device-commands";
        private const string Filter = "command";

        public CommandService(ICommandRepository commandRepository, IMapper mapper, ServiceBusClient serviceBusClient)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
            _serviceBusSender = serviceBusClient.CreateSender(TopicName);
        }

        public async Task<Guid> AddCommandAsync(CommandDTO commandDTO)
        {
            var command = _mapper.Map<Command>(commandDTO);

            await _commandRepository.CreateAsync(command);

            return command.Id;
        }

        public async Task<bool> SendCommandAsync(CommandDTO commandDTO)
        {
            try
            {
                var toSerialize = new
                {
                    DeviceId = commandDTO.DeviceId,
                    CommandName = commandDTO.CommandName,
                    CommandValue = commandDTO.CommandValue
                };

                var commandJson = JsonSerializer.Serialize(toSerialize);

                var message = new ServiceBusMessage(commandJson)
                {
                    ContentType = "application/json",
                };
                message.ApplicationProperties["type"] = Filter;

                await _serviceBusSender.SendMessageAsync(message);
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending command to all devices: {ex.Message}");
                return false;
            }
        }
    }
}
