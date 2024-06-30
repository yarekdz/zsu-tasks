using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Domain.Shared;

namespace Tasks.Application.Validation
{
    public static class ValidationExtensions
    {
        public static MediatRServiceConfiguration AddValidation<TRequest, TResponse>(
            this MediatRServiceConfiguration config) where TRequest : notnull
        {
            return config.AddBehavior<IPipelineBehavior<TRequest, Result<TResponse>>,
                ValidationBehaviour<TRequest, TResponse>>();
        }
    }
}
