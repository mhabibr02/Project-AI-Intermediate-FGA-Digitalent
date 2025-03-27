using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    "your-deployment-name",
    "your-endpoint",
    "your-api-key",
    "deployment-model");
var kernel = builder.Build();

kernel.ImportPluginFromType<ConversationSummaryPlugin>();
var prompts = kernel.ImportPluginFromPromptDirectory("Prompts/TravelPlugins");

ChatHistory history = [];
string input = @"I'm planning an anniversary trip with my spouse. We like hiking, 
    mountains, and beaches. Our travel budget is $15000";

var result = await kernel.InvokeAsync<string>(prompts["SuggestDestinations"],
    new() {{ "input", input }});

Console.WriteLine("Where would you like to go?");
input = Console.ReadLine();

result = await kernel.InvokeAsync<string>(prompts["SuggestActivities"],
    new() {
        { "history", history },
        { "destination", input },
    }
);

Console.WriteLine(result);
Console.WriteLine(result);
history.AddUserMessage(input);
history.AddAssistantMessage(result);

// Absolutely! Japan is a wonderful destination with so much to see and do. Here are some recommendations for activities and points of interest:

// 1. Visit Tokyo Tower: This iconic tower offers stunning views of the city and is a must-visit attraction.

// 2. Explore the temples of Kyoto: Kyoto is home to many beautiful temples, including the famous Kiyomizu-dera and Fushimi Inari-taisha.

// 3. Experience traditional Japanese culture: Attend a tea ceremony, try on a kimono, or take a calligraphy class to immerse yourself in Japanese culture.
