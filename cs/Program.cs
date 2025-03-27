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

// Output
// 1. Bonjour - Hello
// 2. Merci - Thank you
// 3. Oui - Yes
// 4. Non - No
// 5. S'il vous plaît - Please
// 6. Excusez-moi - Excuse me
// 7. Parlez-vous anglais? - Do you speak English?
// 8. Je ne comprends pas - I don't understand
// 9. Pouvez-vous m'aider? - Can you help me? 
// 10. Combien ça coûte? - How much does it cost?
// 11. Où est la gare? - Where is the train station?
