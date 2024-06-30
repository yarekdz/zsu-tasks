using FluentValidation;
using MediatR;
using Tasks.Domain.Shared;

namespace Tasks.Application.Validation
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehaviour(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid) return await next();

            var firstValidationError = validationResult.Errors.First();

            throw new DomainValidationException(Error.Validation(firstValidationError.ErrorCode,
                firstValidationError.ErrorMessage));
        }
    }
}
