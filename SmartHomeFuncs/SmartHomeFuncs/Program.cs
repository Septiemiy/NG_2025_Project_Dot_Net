using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddHttpClient();

builder.Services.AddSingleton((sb) =>
{
    var serviceBusConnectionString = Environment.GetEnvironmentVariable("ServiceBusConnectionString");
    return new ServiceBusClient(serviceBusConnectionString);
});

builder.Build().Run();
