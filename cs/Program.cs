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
string prompt = @$"Create a list of helpful phrases and 
    words in ${language} a traveler would find useful.

    Group phrases by category. Display the phrases in 
    the following format: Hello - Ciao [chow]";

builder.Plugins.AddFromType<ConversationSummaryPlugin>();
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

// Restaurant Phrases:
// - Water, please - De l'eau, s'il vous plaît [duh loh, seel voo pleh]
// - Check, please - L'addition, s'il vous plaît [lah-di-syo(n), seel voo pleh]
// - Bon appétit - Bon appétit [bohn ah-peh-teet]

// Transportation Phrases:
// - Where is the train station? - Où est la gare? [oo-eh lah gahr]
// - How do I get to...? - Comment aller à...? [ko-mahn tah-lay ah]
// - I need a taxi - J'ai besoin d'un taxi [zhay buh-zwan dunn tah-xee]
