using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SmartHomeFuncs.Functions.Command;

public class SBTopicBathroomLights
{
    private const string Topic = "device-commands";
    private const string Subscription = "bathroom-lights";
    private const string Filtr = "room";

    [Function(nameof(SBTopicBathroomLights))]
    public async Task Run(
        [ServiceBusTrigger(Topic, Subscription, Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        var command = message.Body.ToString();
        Console.WriteLine($"Received message: {command}");
        if (message.ApplicationProperties.TryGetValue(Filtr, out var roomValue))
        {
            if (command.Contains("TurnOffLights"))
            {
                //Send to device
                Console.WriteLine($"Bathroom lights turned off: {command}");
                await messageActions.CompleteMessageAsync(message);
            }
        }
    }
}