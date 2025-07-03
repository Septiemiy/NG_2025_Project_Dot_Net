using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SmartHomeFuncs;

public class Function1
{
    [Function(nameof(Function1))]
    public async Task Run(
        [ServiceBusTrigger("myqueue", Connection = "")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        await messageActions.CompleteMessageAsync(message);
    }
}