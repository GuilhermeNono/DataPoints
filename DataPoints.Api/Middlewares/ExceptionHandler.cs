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
        var response = context.HttpContext.Response;

        var error = errorCatcher?.Catch(context.Exception);

        response.ContentType = "application/json";
        context.Result = new JsonResult(error) { StatusCode = error?.First().StatusCode };
    }
}