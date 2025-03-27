using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;

string filePath = Path.GetFullPath("../../appsettings.json");
var config = new ConfigurationBuilder()
    .AddJsonFile(filePath)
    .Build();

// Set your values in appsettings.json
string modelId = config["modelId"]!;
string endpoint = config["endpoint"]!;
string apiKey = config["apiKey"]!;

builder.Plugins.AddFromType<ConversationSummaryPlugin>();
var kernel = builder.Build();

string input = @"I'm a vegan in search of new recipes. I love spicy food! 
Can you give me a list of breakfast recipes that are vegan friendly?";

var result = await kernel.InvokeAsync(
    "ConversationSummaryPlugin", 
    "GetConversationActionItems", 
    new() {{ "input", input }});

Console.WriteLine(result);

var builder = Kernel.CreateBuilder();
builder..AddAzureOpenAIChatCompletion(
    "your-deployment-name",
    "your-endpoint",
    "your-api-key",
    "deployment-model");

var kernel = builder.Build();

string language = "French";
string prompt = @$"Create a list of helpful phrases and 
    words in ${language} a traveler would find useful.";

var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result);
