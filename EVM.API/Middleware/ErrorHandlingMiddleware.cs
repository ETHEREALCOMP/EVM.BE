using EVM.Services.Exceptions;
using EVM.Services.Features.Models.Responses;
using EVM.Services.Service;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EVM.API.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, logger);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "Every custom error should be logged")]
    private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ErrorHandlingMiddleware> logger)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        var code = HttpStatusCode.InternalServerError;
        var message = "Something went wrong. Try a bit later.";

        // TODO: Logging

#if DEBUG
        if (Debugger.IsAttached)
        {
            Debugger.Break();
        }
#endif

        if (exception is BaseCustomException customException)
        {
            code = customException.Code;
            message = customException.Message;

            if (customException.InternalMessage is not null)
            {
                logger.LogError(exception, customException.InternalMessage);
            }
        }

        return context.Response.WriteAsync(JsonSerializer.Serialize<ApiResponse>(
            new(code, message),
            options: new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            }));
    }
}