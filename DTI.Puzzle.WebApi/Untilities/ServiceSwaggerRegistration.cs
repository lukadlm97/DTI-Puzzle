using Microsoft.OpenApi.Models;

namespace DTI.Puzzle.WebApi.Untilities
{
    public static class ServiceSwaggerRegistration
    {
        public static IServiceCollection ConfigureSwaggerServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(configuration.GetSection("ApplicationSettings:Version").Value, new OpenApiInfo
                {
                    Version = configuration.GetSection("ApplicationSettings:Version").Value,
                    Title = configuration.GetSection("ApplicationSettings:Title").Value,

                });

            });
            return services;
        }
    }
}
