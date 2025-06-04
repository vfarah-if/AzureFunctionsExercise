# Azure Functions Exercise
Practise Serverless and get your ducks in a row. So I use Rider and vscode, which means I needed to install stuff and setup stuff Visual Studio is nice enough to give me out of the box.

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools v4+](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) with Azure Functions tools OR VS Code with Azure Functions extension
- An Azure subscription (for deployment)

The following steps use Homebrew to install the Core Tools on macOS.

1. Install [Homebrew](https://brew.sh/), if it's not already installed.

2. Install the Core Tools package:

   Bash

   ```bash
   brew tap azure/functions
   brew install azure-functions-core-tools@4
   # if upgrading on a machine that has 2.x or 3.x installed:
   brew link --overwrite azure-functions-core-tools@4
   ```

To implement **Azure Functions** using **C#**, you can either:

1. Use **in-process** Azure Functions (runs within the Azure Functions host).
2. Use the **isolated worker process model** (runs separately with .NET 6+ / .NET 8).

------

## 🔧 Recommended: .NET 8 Isolated Worker Model (Modern Approach)

This approach gives you full control of the host process and is suitable for microservice-style architectures, which I suspect aligns with your work, Vincent.

------

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools v4+](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) with Azure Functions tools OR VS Code with Azure Functions extension
- An Azure subscription (for deployment)

------

### 📁 Step 1: Create a new Azure Functions Project

```bash
❯func init FunctionExerciseApp --worker-runtime dotnetIsolated --target-framework net8.0
❯cd FunctionExerciseApp
❯func new --name HelloWorldFunction --template "HttpTrigger"
```

This generates:

- A `.csproj` using .NET 8 and isolated worker SDK
- A `Program.cs` that configures the host
- A function like `HelloWorldFunction.cs`

------

### 📄 Step 2: Example Azure Function (C#)

**HelloWorldFunction.cs**

```csharp
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace MyFunctionApp;

public class HelloWorldFunction
{
    private readonly ILogger _logger;

    public HelloWorldFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HelloWorldFunction>();
    }

    [Function("HelloWorld")]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString("Hello, Vincent! Your Azure Function is running.");
        return response;
    }
}
```

------

### 🏁 Step 3: Run Locally

```bash
func start
```

Visit: http://localhost:7071/api/HelloWorld

------

### ☁️ Step 4: Deploy to Azure

#### Option A: From CLI

```bash
az login
az functionapp create --resource-group <your-rg> --consumption-plan-location westeurope --runtime dotnet-isolated --functions-version 4 --name <your-func-name> --storage-account <your-storage>
func azure functionapp publish <your-func-name>
```

#### Option B: From Visual Studio

- Right-click the project → *Publish*
- Choose *Azure Function App (Windows/Linux)* → Select or create a target → Publish

------

## ✅ Observations for Your Architecture Style

Given your emphasis on **clean code, SOLID, and DDD**:

- You can split logic into services and inject them via `DI` in the constructor.
- Define input/output bindings declaratively (e.g., BlobTrigger, QueueTrigger, CosmosDBTrigger).
- Keep functions as orchestration points, delegating real logic to injected services.

------

### Bonus: DI Setup (Program.cs)

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFunctionApp;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IMyService, MyService>();
    })
    .Build();

host.Run();
```

------

Would you like a sample project repo or additional bindings example (e.g., QueueTrigger, TimerTrigger, or BlobTrigger)?





## References

- [Getting Started with Azure Functions: C# and Visual Studio Code Tutorial](https://www.youtube.com/watch?v=Mb_eUDwVHos)

- [Getting Started With Azure Functions - HTTP & Timer Triggers](https://www.youtube.com/watch?v=l3beXs3o-0w)

.
