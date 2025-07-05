using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeFuncs.Functions
{
    public class SBTopicKitchenLights
    {
        private const string Topic = "device-commands";
        private const string Subscription = "kitchen-lights";
        private const string Filtr = "room";

        [Function(nameof(SBTopicKitchenLights))]
        public async Task Run(
            [ServiceBusTrigger(Topic, Subscription, Connection = "ServiceBusConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            var command = message.Body.ToString();
            Console.WriteLine($"Received message: {command}");
            if (message.ApplicationProperties.TryGetValue(Filtr, out var roomValue))
            {
                if(command.Contains("TurnOffLights"))
                {
                    //Send to device
                    Console.WriteLine($"Kitchen lights turned off: {command}");
                    await messageActions.CompleteMessageAsync(message);
                }
            }
        }
    }
}
