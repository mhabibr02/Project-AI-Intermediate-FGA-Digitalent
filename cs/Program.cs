using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder();
builder.AddAzureOpenAIChatCompletion(
  "your-deployment-name",
  "your-endpoint",
  "your-api-key",
  "deployment-model");

var kernel = builder.Build();

kernel.ImportPluginFromType<MusicLibraryPlugin>();
kernel.ImportPluginFromType<MusicConcertPlugin>();
kernel.ImportPluginFromPromptDirectory("Prompts");

OpenAIPromptExecutionSettings settings = new()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};

var songSuggesterFunction = kernel.CreateFunctionFromPrompt(
    promptTemplate: @"Based on the user's recently played music:
        {{$recentlyPlayedSongs}}
        recommend a song to the user from the music library:
        {{$musicLibrary}}",
    functionName: "SuggestSong",
    description: "Recommend a song from the library"
);

kernel.Plugins.AddFromFunctions("SuggestSong", [songSuggesterFunction]);

string prompt = @"Add this song to the recently played songs list:  title: 'Touch', artist: 'Cat's Eye', genre: 'Pop'";

var result = await kernel.InvokePromptAsync(prompt, new(settings));

Console.WriteLine(result);

// I have added the song 'Touch' by Cat's Eye to the recently played songs list.
