using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Application.Abstractions.Data;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Persistence.Repositories.Commands;
using Tasks.Persistence.Repositories.Queries;

namespace Tasks.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("Database")));

            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ITaskCommandsRepository, TaskCommandsRepository>();
            services.AddScoped<ITaskQueriesRepository, TaskQueriesRepository>();

            return services;
        }
    }
}
