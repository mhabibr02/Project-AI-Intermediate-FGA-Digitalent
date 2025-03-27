using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;
    
var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    "your-deployment-name",
    "your-endpoint",
    "your-api-key",
    "deployment-model");
builder.Plugins.AddFromType<TimePlugin>();
var kernel = builder.Build();
var currentDay = await kernel.InvokeAsync("TimePlugin", "DayOfWeek");
Console.WriteLine(currentDay);
