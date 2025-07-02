using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ServiceBusFunction;

public class ServiceBusTrigger
{
    private const string QueueName = "device-data";
    private readonly HttpClient _httpClient;

    public ServiceBusTrigger(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [Function(nameof(ServiceBusTrigger))]
    public async Task Run(
        [ServiceBusTrigger(QueueName, Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        Console.WriteLine($"Received message: {message.Body.ToString()}");

        await messageActions.CompleteMessageAsync(message);
    }
}