var chordProgressionPlugin = kernel.CreatePluginFromPromptDirectory("Prompts/ChordProgressions");
string input = "G, C";

var result = await kernel.InvokeAsync(
    plugins["SuggestChords"],
    new() {{ "startingChords", input }});

Console.WriteLine(result);
