namespace SmartService.Controllers;

using System.Net;
using Core.ExecutionResult;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class BaseApiController : ControllerBase
{
    protected IActionResult FromExecutionResult(ExecutionResult executionResult)
    {
        if (executionResult.Success)
        {
            return Ok(executionResult);
        }

        var status = executionResult.Status == default
            ? (int)HttpStatusCode.BadRequest
            : executionResult.Status;

        return StatusCode(status, executionResult);
    }

    protected IActionResult FromExecutionResult<T>(ExecutionResult<T> executionResult)
    {
        if (executionResult.Success)
        {
            return Ok(executionResult);
        }

        var status = executionResult.Status == default
            ? (int)HttpStatusCode.BadRequest
            : executionResult.Status;

        return StatusCode(status, executionResult);
    }
    
}