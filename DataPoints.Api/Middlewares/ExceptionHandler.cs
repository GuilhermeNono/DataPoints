using DataPoints.Domain.Errors;
using DataPoints.Domain.Errors.Abstractions.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DataPoints.Api.Middlewares;

public class ExceptionHandler : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var errorCatcher = context.HttpContext.RequestServices.GetService<IErrorCatcher>();
        var error = errorCatcher?.Catch(context.Exception).First();

        var statusCode = error?.StatusCode ?? StatusCodes.Status500InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Type = $"https://httpstatuses.io/{statusCode}",
            Title = error?.Code ?? "InternalServerError",
            Status = statusCode,
            Detail = error?.Description,
            Instance = context.HttpContext.Request.Path,
        };

        if (context.HttpContext.Items.TryGetValue(CorrelationIdMiddleware.HeaderName, out var correlationId))
            problemDetails.Extensions["traceId"] = correlationId;

        if (error is ValidationError { Errors: not null } validationError)
            problemDetails.Extensions["errors"] = validationError.Errors.Select(e => e.Description);

        context.HttpContext.Response.ContentType = "application/problem+json";
        context.Result = new JsonResult(problemDetails) { StatusCode = statusCode };
    }
}
