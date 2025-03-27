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
kernel.ImportPluginFromType<MusicLibraryPlugin>();

string history = @"In the heart of my bustling kitchen, I have embraced the challenge 
    of satisfying my family's diverse taste buds and navigating their unique tastes. 
    With a mix of picky eaters and allergies, my culinary journey revolves around 
    exploring a plethora of vegetarian recipes.

    One of my kids is a picky eater with an aversion to anything green, while another 
    has a peanut allergy that adds an extra layer of complexity to meal planning. 
    Armed with creativity and a passion for wholesome cooking, I've embarked on a 
    flavorful adventure, discovering plant-based dishes that not only please the 
    picky palates but are also heathy and delicious.";

kernel.ImportPluginFromType<ConversationSummaryPlugin>();

string prompt = @"User information: 
    {{ConversationSummaryPlugin.SummarizeConversation $history}}

    Given this user's background information, provide a list of relevant recipes.";

var result = await kernel.InvokePromptAsync(suggestRecipes, new() { "history", history });
Console.WriteLine(result);
    }
);

Console.WriteLine(result);
