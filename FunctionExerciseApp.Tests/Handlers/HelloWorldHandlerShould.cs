using AzureFunctions.Domain.Handlers;
using AzureFunctions.Domain.Services;
using Moq;

namespace FunctionExerciseApp.Tests.Handlers;

public class HelloWorldHandlerShould
{
    [Fact]
    public async Task ReturnExpectedGreeting()
    {
        var mockGreetingService = new Mock<IGreetingService>();
        mockGreetingService
            .Setup(greetingService => greetingService.GetGreeting("Vincent"))
            .Returns("Hello Vincent!");
        var handler = new HelloWorldHandler(mockGreetingService.Object);

        var actual = await handler.HandleAsync("Vincent");

        Assert.Equal("Hello Vincent!", actual);
    }
}