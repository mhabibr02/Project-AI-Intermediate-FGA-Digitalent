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
kernel.ImportPluginFromType<TodoListPlugin>();

var result = await kernel.InvokeAsync<string>(
  "TodoListPlugin", 
  "CompleteTask", 
  new() {{ "task", "Buy groceries" }}
);
Console.WriteLine(result);
