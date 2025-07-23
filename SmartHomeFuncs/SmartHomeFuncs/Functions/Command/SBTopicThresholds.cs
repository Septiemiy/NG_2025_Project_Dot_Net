using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SmartHomeFuncs.Functions.Command.Models;

namespace SmartHomeFuncs.Functions.Command;

public class SBTopicThresholds
{
    private const string Topic = "device-commands";
    private const string Subscription = "Thresholds";
    private const string Filter = "threshold";

    [Function(nameof(SBTopicThresholds))]
    public async Task Run(
        [ServiceBusTrigger(Topic, Subscription, Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        try
        {
            var thresholdDTO = JsonSerializer.Deserialize<ThresholdDTO>(message.Body.ToString());
            await messageActions.CompleteMessageAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR:", ex);
            await messageActions.AbandonMessageAsync(message);
        }
    }
}