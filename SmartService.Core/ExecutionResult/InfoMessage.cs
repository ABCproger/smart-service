namespace SmartService.Core.ExecutionResult;

public class InfoMessage
{
    public InfoMessage()
    {
    }

    public InfoMessage(string message)
    {
        Message = message;
    }

    public InfoMessage(string title, string message)
    {
        Title = title;
        Message = message;
    }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}