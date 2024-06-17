using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Tasks.Api.Mapper
{
    public static class MapsterDi
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;

            //services.AddSingleton(() =>  //Добавляем конфиг
            //{
            //    var config = new TypeAdapterConfig();

            //    new RegisterMaps().Register(config);

            //    return config;
            //});
            //services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
