case "SuggestDestinations":
    chatHistory.AppendLine("User:" + input);
    var recommendations = await kernel.InvokePromptAsync(input!);
    Console.WriteLine(recommendations);
    break;
