using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
  "your-deployment-name",
  "your-endpoint",
  "your-api-key",
  "deployment-model");

var kernel = builder.Build();

kernel.ImportPluginFromType<MusicLibraryPlugin>();
kernel.ImportPluginFromType<MusicConcertPlugin>();
kernel.ImportPluginFromPromptDirectory("Prompts");
kernel.ImportPluginFromType<CurrencyConverter>();
var prompts = kernel.ImportPluginFromPromptDirectory("Prompts");

OpenAIPromptExecutionSettings settings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

Console.WriteLine("What would you like to do?");
var input = Console.ReadLine();
var intent = await kernel.InvokeAsync<string>(
    prompts["GetIntent"], 
    new() {{ "input",  input }}
);

switch (intent) {
    case "ConvertCurrency": 
        // ...Code you entered previously...
        break;
    case "SuggestDestinations":
    case "SuggestActivities":
    case "HelpfulPhrases":
    case "Translate":
        var autoInvokeResult = await kernel.InvokePromptAsync(input!, new(settings));
        Console.WriteLine(autoInvokeResult);
        break;
    default:
        Console.WriteLine("Other intent detected");
        break;
}
