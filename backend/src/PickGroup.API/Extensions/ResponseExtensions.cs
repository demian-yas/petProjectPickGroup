using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using PickGroup.API.Response;
using PickGroup.Application.Common.Pagination;
using PickGroup.Domain.Common;

namespace PickGroup.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = error.ErrorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = Envelope.Error(error);
        
        return new ObjectResult(response)
        {
            StatusCode = statusCode
        };
    }
    
    
    public static ActionResult ToResponse<T>(this Result<T, Error> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(Envelope.Ok(result.Value));
        }

        return result.Error.ToResponse();
    }

    public static ActionResult ToResponse<T>(this Result<PagedResult<T>, Error> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(Envelope.Ok(PagedResponse<T>.ToPagedResponse(result.Value)));
        }

        return result.Error.ToResponse();
    }
}