namespace SmartService.Core.ExecutionResult;

public class ExecutionResult<T> : ExecutionResult
{
    public ExecutionResult()
    {
    }

    public ExecutionResult(int status, ErrorInfo error)
        : base(status, error)
    {
    }

    public ExecutionResult(ErrorInfo error)
        : base(error)
    {
    }

    public ExecutionResult(InfoMessage message)
        : base(message)
    {
    }
    
    public ExecutionResult(ExecutionResult result)
        : base(result)
    {
    }

    public ExecutionResult(T result)
    {
        Value = result;
    }

    public ExecutionResult(T result, InfoMessage message)
    {
        Messages.Add(message);
        Value = result;
    }

    public T Value { get; set; } = default!;
}