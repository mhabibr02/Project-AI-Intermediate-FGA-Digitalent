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
string input = @"I have a vacation from June 1 to July 22. I want to go to Greece. 
    I live in Chicago.";

// Assign a persona to the prompt
string prompt = @$"
<message role=""system"">Instructions: Identify the from and to destinations 
and dates from the user's request</message>

<message role=""user"">Can you give me a list of flights from Seattle to Tokyo? 
I want to travel from March 11 to March 18.</message>

<message role=""assistant"">Seattle|Tokyo|03/11/2024|03/18/2024</message>

<message role=""user"">${input}</message>";
var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result);

// Output

// Chicago|Greece|06/01/2024|07/22/2024
