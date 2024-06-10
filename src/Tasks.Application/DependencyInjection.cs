using Microsoft.Extensions.DependencyInjection;
using Tasks.Application.Abstractions.Data;

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
            });

            return services;
        }
    }
}
