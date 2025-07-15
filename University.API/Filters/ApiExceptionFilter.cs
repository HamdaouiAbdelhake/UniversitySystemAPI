using AutoWrapper.Wrappers;
using Azure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using University.Core.Exceptions;

namespace University.API.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NotFoundException:
                context.Result = Response(context.Exception.Message, "Item not found", StatusCodes.Status404NotFound);
                break;
            case BusinessException businessException:
                if (businessException.Errors.Any())
                {
                    context.Result = Response(businessException.Errors, "One or more validation errors",
                        StatusCodes.Status400BadRequest);
                }
                else
                {
                    context.Result = Response(businessException.Message, "One or more validation errors",
                        StatusCodes.Status400BadRequest);
                }
                break;
            default:
                context.Result = Response(context.Exception.Message, "Internal server Error",
                    StatusCodes.Status500InternalServerError);
                break;
        }
    }

    public ObjectResult Response(string message, string title, int status, string? stacktrace = null)
    {
        var result = new ApiResponse
        {
            StatusCode = status,
            Message = message,
            ResponseException = title,
            IsError = true,
            Version = "1.0",
            Result = stacktrace
        };

        return new ObjectResult(result)
        {
            StatusCode = status
        };
    }

    public ObjectResult Response(Dictionary<string, List<string>> errors, string title, int status)
    {
        var result = new ApiResponse
        {
            StatusCode = status,
            Message = title,
            ResponseException = title,
            IsError = true,
            Version = "1.0",
            Result = errors
        };

        return new ObjectResult(result)
        {
            StatusCode = status
        };
    }
}