namespace SmartService.Core.ExecutionResult;

public class ErrorInfo
{
    public ErrorInfo()
    {
    }

    public ErrorInfo(string message)
    {
        Message = message;
    }

    public ErrorInfo(string key, string message)
    {
        Key = key;
        Message = message;
    }

    public string Key { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Key}:{Message}";
    }
}