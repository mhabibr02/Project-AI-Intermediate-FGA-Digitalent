Console.WriteLine(
    await kernel.InvokePromptAsync(generateNamesOfPrompt, new() {{ "input", "fantasy characters" }})
);
