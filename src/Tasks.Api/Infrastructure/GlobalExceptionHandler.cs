using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tasks.Domain.Shared;

namespace Tasks.Api.Infrastructure
{
    internal sealed class GlobalExceptionHandler
        : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);

            ProblemDetails problemDetails;
            switch (exception)
            {
                case DomainValidationException domainValidationException:
                    problemDetails = new ProblemDetails
                    {
                        Type = domainValidationException.Error.Type.ToString(),
                        Title = domainValidationException.Error.Code,
                        Detail = domainValidationException.Error.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                    };

                    var error = domainValidationException.Error;
                    problemDetails.Status = error.Type switch
                    {
                        ErrorType.Failure => StatusCodes.Status500InternalServerError,
                        ErrorType.Validation => StatusCodes.Status400BadRequest,
                        ErrorType.Conflict => StatusCodes.Status409Conflict,
                        ErrorType.NotFound => StatusCodes.Status404NotFound,
                        _ => problemDetails.Status
                    };

                    _logger.LogError(exception, "Domain exception occurred: Type: {Type} | Code: {Code} | Message: {Message}",
                        error.Type.ToString(), error.Code, error.Message);
                    break;
                //todo: add more exceptions
                default:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Server failure",
                        Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                        Detail = exception.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                    };

                    _logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);
                    break;
            }

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
