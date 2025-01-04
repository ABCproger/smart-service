namespace SmartService.Core.ExecutionResult;

public class ExecutionResult
{
    public ExecutionResult()
    {
    }

    public ExecutionResult(int status, ErrorInfo error)
    {
        Status = status;
        Success = false;
        Errors.Add(error);
    }

    public ExecutionResult(ErrorInfo error)
    {
        Success = false;
        Status = Status == 200 ? 400 : Status;
        Errors.Add(error);
    }

    public ExecutionResult(InfoMessage message)
    {
        Messages.Add(message);
    }
    
    public ExecutionResult(ExecutionResult result)
    {
        Success = result.Success;
        Status = result.Status;
        Errors.AddRange(result.Errors);
        Messages.AddRange(result.Messages);
    }

    public int Status { get; set; } = 200;
    public bool Success { get; set; } = true;


    public List<ErrorInfo> Errors { get; } = new ();
    public List<InfoMessage> Messages { get; } = new ();
}