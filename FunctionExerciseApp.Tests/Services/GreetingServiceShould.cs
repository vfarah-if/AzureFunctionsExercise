using AzureFunctions.Domain.Services;

namespace FunctionExerciseApp.Tests.Services;

public class GreetingServiceShould
{
    [Fact]
    public void ReturnGreeting()
    {
        var service = new GreetingService();
        var name = "You";

        var result = service.GetGreeting(name);

        Assert.Equal("Hello, You! This is an Http triggered Azure Function at heart.", result);
    }
}