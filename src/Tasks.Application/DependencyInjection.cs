using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Application.Abstractions.Data;
using Tasks.Application.Validation;

namespace Tasks.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddMediatR(configuration =>
                {
                    configuration.RegisterServicesFromAssemblyContaining<AssemblyReference>();

                    configuration.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
                })
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly,
                includeInternalTypes: true);

            return services;
        }
    }
}
