using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TheXReasonPodcast.Application.Installers
{
    public static class SwaggerInstaller
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TheXReasonPodcast.Api", Version = "v1" });
            });

            return services;
        }
    }
}