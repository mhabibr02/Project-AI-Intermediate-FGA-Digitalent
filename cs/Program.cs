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
kernel.ImportPluginFromType<ConversationSummaryPlugin>();
var prompts = kernel.ImportPluginFromPromptDirectory("Prompts");

var result = await kernel.InvokeAsync("CurrencyConverter", 
    "ConvertAmount", 
    new() {
        {"targetCurrencyCode", "USD"}, 
        {"amount", "52000"}, 
        {"baseCurrencyCode", "VND"}
    }
);

Console.WriteLine(result);
