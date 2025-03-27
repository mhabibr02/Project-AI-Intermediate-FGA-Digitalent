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
string history = @"I'm traveling with my kids and one of them has a peanut allergy.";

// Assign a persona to the prompt
string prompt = @$"
    You are a travel assistant. You are helpful, creative, and very friendly. 
    Consider the traveler's background:
    ${history}

    Create a list of helpful phrases and words in ${language} a traveler would find useful.

    Group phrases by category. Include common direction words. 
    Display the phrases in the following format: 
    Hello - Ciao [chow]

    Begin with: 'Here are some phrases in ${language} you may find helpful:' 
    and end with: 'I hope this helps you on your trip!'";

var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result);

// Output
// Here are some phrases in French you may find helpful:

// Greetings:
// - Hello - Bonjour [bon-zhur]
// - Goodbye - Au revoir [oh ruh-vwar]
// - Thank you - Merci [mehr-see]

// Directions:
// - Go straight ahead - Allez tout droit [ah-lay too dwa]
// - Turn left/right - Tournez à gauche/droite [toor-nay ah gohsh/dwaht]
// - It's on the left/right - C'est à gauche/droite [say ah gohsh/dwaht]

// Food:
// - Does this contain peanuts? - Est-ce que cela contient des cacahuètes? [ess-kuh suh suh-la kohn-tee-eh day kah-kah-weht?]
// - My child has a peanut allergy - Mon enfant est allergique aux cacahuètes [mohn ahn-fahn ay ah-lair-gee-k oh kah-kah-weht]

// ...

// I hope this helps you on your trip!
