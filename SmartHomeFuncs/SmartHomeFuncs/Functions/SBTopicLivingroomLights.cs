using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SmartHomeFuncs.Functions;

public class SBTopicLivingroomLights
{
    private const string Topic = "device-commands";
    private const string Subscription = "livingroom-lights";
    private const string Filtr = "room";

    [Function(nameof(SBTopicLivingroomLights))]
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
                Console.WriteLine($"Livingroom lights turned off: {command}");
                await messageActions.CompleteMessageAsync(message);
            }
        }
    }
}