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

var result = await kernel.InvokeAsync(prompts["GetTargetCurrencies"],
    new() {
        {"input", "How many Australian Dollars is 140,000 Korean Won worth?"}
    }
);

Console.WriteLine(result);

// AUD|KRW|140000
