case "SuggestActivities":
    var chatSummary = await kernel.InvokeAsync(
        "ConversationSummaryPlugin", 
        "SummarizeConversation", 
        new() {{ "input", chatHistory.ToString() }});
    break;
var activities = await kernel.InvokePromptAsync(
    input,
    new () {
        {"input", input},
        {"history", chatSummary},
        {"ToolCallBehavior", ToolCallBehavior.AutoInvokeKernelFunctions}
});

chatHistory.AppendLine("User:" + input);
chatHistory.AppendLine("Assistant:" + activities.ToString());

Console.WriteLine(activities);
break;
