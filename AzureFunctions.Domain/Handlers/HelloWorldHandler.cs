using AzureFunctions.Domain.Services;

namespace AzureFunctions.Domain.Handlers;

public interface IHelloWorldHandler
{
    Task<string> HandleAsync(string name);
}

public class HelloWorldHandler(IGreetingService greetingService) : IHelloWorldHandler
{
    public Task<string> HandleAsync(string name = "Vincent")
    {
        return Task.FromResult(greetingService.GetGreeting(name));
    }
}