using System;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using SmartHomeFuncs.Functions.Command.Models;

namespace SmartHomeFuncs.Functions.Command;

public class SBTopicCommands
{
    private const string Topic = "device-commands";
    private const string Subscription = "Commands";
    private const string Filter = "command";

    [Function(nameof(SBTopicCommands))]
    public async Task Run(
        [ServiceBusTrigger(Topic, Subscription, Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        try
        {
            var commandDTO = JsonSerializer.Deserialize<CommandDTO>(message.Body.ToString());
            await messageActions.CompleteMessageAsync(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR:", ex);
            await messageActions.AbandonMessageAsync(message);
        }
    }
}