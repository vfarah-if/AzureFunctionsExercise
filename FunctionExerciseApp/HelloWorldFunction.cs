using System.Diagnostics.CodeAnalysis;
using System.Net;
using AzureFunctions.Domain.Handlers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;

namespace FunctionExerciseApp;

[ExcludeFromCodeCoverage(Justification = "Thin glue, Azure-only concerns tested by Microsoft")]
public class HelloWorldFunction(ILogger<HelloWorldFunction> logger, IHelloWorldHandler handler)
{
    [Function("HelloWorldFunction")]
    // InCorrect Way in Azure Functions Isolated Worker
    // public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        var message = await handler.HandleAsync("Vincent");
        // InCorrect Way in Azure Functions Isolated Worker
        // return new OkObjectResult(message);
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync(message);
        return response;
    }
}
