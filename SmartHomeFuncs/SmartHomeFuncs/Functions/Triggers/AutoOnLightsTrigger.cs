using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SmartHomeFuncs.Functions.Command.Models;

namespace SmartHomeFuncs.Functions.Triggers;

public class AutoOnLightsTrigger
{
    private readonly ServiceBusSender _serviceBusSender;
    private const string TopicName = "device-commands";

    public AutoOnLightsTrigger(ServiceBusClient serviceBusClient)
    {
        _serviceBusSender = serviceBusClient.CreateSender(TopicName);
    }

    [Function(nameof(AutoOnLightsTrigger))]
    public async Task Run([TimerTrigger("0 0 7 * * *")] TimerInfo myTimer)
    {
        var command = new CommandDTO
        {
            DeviceId = "auto-lights",
            CommandName = "Turn On",
        };

        var message = new ServiceBusMessage(JsonSerializer.Serialize(command))
        {
            ContentType = "application/json"
        };

        await _serviceBusSender.SendMessageAsync(message);
    }
}