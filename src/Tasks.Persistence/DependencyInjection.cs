using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Tasks.Application.Abstractions.Data;
using Tasks.Domain.Abstractions.Repositories.Commands;
using Tasks.Domain.Abstractions.Repositories.Queries;
using Tasks.Persistence.Data;
using Tasks.Persistence.Health;
using Tasks.Persistence.Options;
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
            services.AddOptions<ConnectionStringOptions>()
                .Bind(configuration.GetSection(ConnectionStringOptions.Position));

            var npgSqlConnectionString = configuration.GetConnectionString("Database");

            services.AddScoped<NpgSqlDbConnectionFactory, NpgSqlDbConnectionFactory>();

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(npgSqlConnectionString
                        //,x => x.MigrationsAssembly()
                        ));

            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ITaskCommandsRepository, TaskCommandsRepository>();
            services.AddScoped<ITaskQueriesRepository, TaskQueriesRepository>();

            services.AddHealthChecks()
                .AddNpgSql(npgSqlConnectionString)
                .AddDbContextCheck<ApplicationDbContext>();

            return services;
        }
    }
}
