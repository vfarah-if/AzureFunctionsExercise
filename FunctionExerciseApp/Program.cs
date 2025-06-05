using AzureFunctions.Domain.Handlers;
using AzureFunctions.Domain.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();
builder.Services
    .AddSingleton<IGreetingService, GreetingService>()
    .AddSingleton<IHelloWorldHandler, HelloWorldHandler>();
builder.Build().Run();
