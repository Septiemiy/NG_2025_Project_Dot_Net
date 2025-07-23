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
    public class ThresholdService : IThresholdService
    {
        private readonly IThresholdRepository _thresholdRepository;
        private readonly IMapper _mapper;
        private readonly ServiceBusSender _serviceBusSender;
        private const string TopicName = "device-commands";
        private const string Filter = "threshold";

        public ThresholdService(IThresholdRepository thresholdRepository, IMapper mapper, ServiceBusClient serviceBusClient)
        {
            _thresholdRepository = thresholdRepository;
            _mapper = mapper;
            _serviceBusSender = serviceBusClient.CreateSender(TopicName);
        }

        public async Task<Guid> AddThresholdAsync(ThresholdDTO thresholdDTO)
        {
            var threshold = _mapper.Map<Threshold>(thresholdDTO);

            await _thresholdRepository.CreateAsync(threshold);

            return threshold.Id;
        }

        public async Task<bool> SendThresholdAsync(ThresholdDTO thresholdDTO)
        {
            try
            {
                var toSerialize = new
                {
                    DeviceId = thresholdDTO.DeviceId,
                    ThresholdCondition = thresholdDTO.ThresholdCondition,
                    Value = thresholdDTO.Value,
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

        public async Task<bool> isThresholdExists(ThresholdDTO thresholdDTO)
        {
            var threshold = _mapper.Map<Threshold>(thresholdDTO);

            var existingThreshold = await _thresholdRepository.GetThresholdByConditionAndValueAsync(threshold.ThresholdCondition, threshold.Value);

            if (existingThreshold != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
