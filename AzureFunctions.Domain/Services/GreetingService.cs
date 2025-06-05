
namespace AzureFunctions.Domain.Services;

public interface IGreetingService
{
    string GetGreeting(string name);
}

public class GreetingService : IGreetingService
{

    public string GetGreeting(string name) => $"Hello, {name}! This is an Http triggered Azure Function at heart.";
}