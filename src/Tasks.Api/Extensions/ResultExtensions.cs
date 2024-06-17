using Microsoft.AspNetCore.Http.HttpResults;
using Tasks.Domain.Shared;

namespace Tasks.Api.Extensions
{
    public static class ResultExtensions
    {
        public static ProblemHttpResult ToProblemDetails(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException("Can't convert success result to problem");
            }

            return TypedResults.Problem(
                statusCode: StatusCode(result.Error.Type),
                title: Title(result.Error.Type),
                type: Type(result.Error.Type),
                extensions: new Dictionary<string, object?>
                {
                    { "errors", new[] { result.Error } }
                }
            );

            static int StatusCode(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => StatusCodes.Status400BadRequest,
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    ErrorType.Conflict => StatusCodes.Status409Conflict,
                    _ => StatusCodes.Status500InternalServerError
                };

            static string Title(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => "Bad Request",
                    ErrorType.NotFound => "Not Found",
                    ErrorType.Conflict => "Conflict",
                    _ => "Server Failure"
                };

            static string Type(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                    ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                    _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
                };
        }
    }
}
