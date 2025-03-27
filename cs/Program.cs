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
string prompt = @$"
    The following is a conversation with an AI travel assistant. 
    The assistant is helpful, creative, and very friendly.

    <message role=""user"">Can you give me some travel destination suggestions?</message>

    <message role=""assistant"">Of course! Do you have a budget or any specific 
    activities in mind?</message>

    <message role=""user"">${input}</message>";
    
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
string input = @"I'm planning an anniversary trip with my spouse. We like hiking, mountains, 
    and beaches. Our travel budget is $15000";

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

// That sounds like a great trip ahead! Here are a few suggestions:

// 1. New Zealand - With stunning mountain ranges, iconic hiking trails, and beautiful beaches, New Zealand is a popular destination for outdoor enthusiasts. Some must-visit spots include the Milford Track, Fox Glacier, and Abel Tasman National Park.

// 2. Hawaii - Known for its picturesque beaches, Hawaii is also home to several stunning hiking trails. The Kalalau Trail on Kauai is a popular trail that offers breathtaking views of the Na Pali Coast.

// 3. Costa Rica - Costa Rica boasts beautiful beaches and breathtaking mountains. Hike through the Monteverde Cloud Forest Reserve and catch a glimpse of exotic wildlife like the resplendent quetzal, or take a dip in the turquoise waters of Playa Manuel Antonio.

// 4. Banff National Park, Canada - Located in the Canadian Rockies, Banff National Park offers some of the most stunning mountain scenery in the world. Explore the park's many hiking trails, relax in hot springs, and take in the beauty of the Canadian wilderness.

// 5. Amalfi Coast, Italy - The Amalfi Coast is a picturesque stretch of coastline in Southern Italy that offers stunning views of the Mediterranean Sea. Take a hike along the famous Path of the Gods or enjoy a romantic stroll through one of the Amalfi Coast's charming towns like Positano or Ravello.

// These are just a few of many options, but with a budget of $15000, you should be able to have a fantastic trip to any of these destinations!

