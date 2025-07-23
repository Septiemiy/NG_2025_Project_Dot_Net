using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SmartHomeFuncs.Functions.Command.Models;

namespace SmartHomeFuncs.Functions.Triggers;

public class SmokeAutoTestTrigger
{
    private readonly ServiceBusSender _serviceBusSender;
    private const string TopicName = "device-commands";

    public SmokeAutoTestTrigger(ServiceBusClient serviceBusClient)
    {
        _serviceBusSender = serviceBusClient.CreateSender(TopicName);
    }

    [Function(nameof(SmokeAutoTestTrigger))]
    public async Task Run([TimerTrigger("0 0 12 * * 0")] TimerInfo myTimer)
    {
        var command = new CommandDTO
        {
            DeviceId = "smoke-autocheck",
            CommandName = "Test Alarm",
        };

        var message = new ServiceBusMessage(JsonSerializer.Serialize(command))
        {
            ContentType = "application/json"
        };

        await _serviceBusSender.SendMessageAsync(message);
    }
}