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

string prompt = @"This is a list of music available to the user:
    {{MusicLibraryPlugin.GetMusicLibrary}} 

    This is a list of music the user has recently played:
    {{MusicLibraryPlugin.GetRecentPlays}}

    Based on their recently played music, suggest a song from
    the list to play next";

var result = await kernel.InvokePromptAsync(prompt);
Console.WriteLine(result);
    }
);

