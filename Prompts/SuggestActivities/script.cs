case "SuggestActivities":
    var chatSummary = await kernel.InvokeAsync(
        "ConversationSummaryPlugin", 
        "SummarizeConversation", 
        new() {{ "input", chatHistory.ToString() }});
    break;
