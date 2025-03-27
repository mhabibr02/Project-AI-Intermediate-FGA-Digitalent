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
string language = "French";
string history = @"I'm traveling with my kids and one of them 
    has a peanut allergy.";
string prompt = @$"Consider the traveler's background:
    ${history}

    Create a list of helpful phrases and words in 
    ${language} a traveler would find useful.

    Group phrases by category. Include common direction 
    words. Display the phrases in the following format: 
    Hello - Ciao [chow]";

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

// Output
// Phrases for dealing with peanut allergy:
// My child has a peanut allergy - Mon enfant a une allergie aux arachides [mon on-fon ah oon ah-lair-zhee oh a-rah-sheed]
// Is there a peanut-free option available? - Y a-t-il une option sans arachide? [ee ah-teel une oh-pee-syon sahn ah-rah-sheed]

// Phrases for directions:
// Turn left - Tournez à gauche [toor-nay ah gohsh]
// Turn right - Tournez à droite [toor-nay ah dwaht]
