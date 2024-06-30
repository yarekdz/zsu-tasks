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
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

            services.AddMediatR(configuration =>
                {
                    configuration.RegisterServicesFromAssemblyContaining<AssemblyReference>();
                    //.AddValidation<CreateTaskCommand, TodoTask>();

                    configuration.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
                })
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
