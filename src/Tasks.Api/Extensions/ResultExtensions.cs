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

            return result switch
            {
                IValidationResult validationResult => CreateProblem(
                    StatusCodes.Status400BadRequest,
                    IValidationResult.ValidationError.Code,
                    ErrorType.Validation,
                    new Dictionary<string, object?>
                    {
                        { nameof(validationResult.Errors), validationResult.Errors }
                    }),

                _ => CreateProblem(
                    StatusCode(result.Error.Type),
                    Title(result.Error.Type),
                    result.Error.Type,
                    new Dictionary<string, object?>
                    {
                        { nameof(result.Error), new[] { result.Error } }
                    })
            };
        }

        private static ProblemHttpResult CreateProblem(
            int statusCode,
            string title,
            ErrorType errorType,
            Dictionary<string, object?> extensions) =>
            TypedResults.Problem(
                statusCode: statusCode,
                title: title,
                type: Type(errorType),
                extensions: extensions
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
