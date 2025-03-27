using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
    "your-deployment-name",
    "your-endpoint",
    "your-api-key",
    "deployment-model");

kernel.ImportPluginFromType<IngredientsPlugin>();
kernel.ImportPluginFromPromptDirectory("Prompts/IngredientPrompts");

// Set the ToolCallBehavior property
OpenAIPromptExecutionSettings settings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

string prompt = @"What ingredients am I missing from my current list of ingredients 
    to make a recipe for aloo jeera?";

// Use the settings to automatically invoke plugins based on the prompt
var result = await kernel.InvokePromptAsync(prompt, new(settings));

Console.WriteLine(result);
