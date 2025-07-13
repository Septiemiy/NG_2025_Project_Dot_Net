using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SmartHomeFuncs.Functions.Telemetry;

public class SBQueueTelemetry
{
    private const string QueueName = "device-data";
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public SBQueueTelemetry(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
    }

    [Function(nameof(SBQueueTelemetry))]
    public async Task Run(
        [ServiceBusTrigger(QueueName, Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        var content = new StringContent(
            message.Body.ToString(),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var resposnse = await _httpClient.PostAsync(_configuration["BFFTelemetryEndpointUrl"], content);

        if (resposnse.IsSuccessStatusCode)
        {
            await messageActions.CompleteMessageAsync(message);
        }
        else
        {
            await messageActions.AbandonMessageAsync(message);
        }

    }
}