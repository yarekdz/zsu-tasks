
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Carter;
using HealthChecks.UI.Client;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Tasks.Api.Extensions;
using Tasks.Api.Infrastructure;
using Tasks.Api.Mapper;
using Tasks.Api.OpenApi;
using Tasks.Application;
using Tasks.Persistence;
using Tasks.Presentation;

namespace Tasks.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddApplication()
                .AddPersistence(builder.Configuration)
                .AddPresentation();

            builder.Services.AddStackExchangeRedisCache(options =>
                options.Configuration = builder.Configuration.GetConnectionString("Cache"));

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddHealthChecks()
                .AddRedis(builder.Configuration.GetConnectionString("Cache"));

            builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

            builder.Services.AddCarter();

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddMappings();

            var app = builder.Build();

            ApiVersionSet apiVersionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .ReportApiVersions()
                .Build();

            var versionedGroup = app
                .MapGroup("api/v{version:apiVersion}")
                .WithApiVersionSet(apiVersionSet);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

                    foreach (var description in descriptions)
                    {
                        string url = $"{description.GroupName}/swagger.json";
                        string name = description.GroupName.ToUpperInvariant();

                        options.SwaggerEndpoint(url, name);
                    }
                });

                app.ApplyMigrations();
            }

            app.MapHealthChecks("health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            app.UseRequestContextLogging();

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseExceptionHandler();

            //todo
            //app.UseAuthorization();

            app.MapControllers();

            app.MapCarter();

            app.Run();
        }
    }
}
