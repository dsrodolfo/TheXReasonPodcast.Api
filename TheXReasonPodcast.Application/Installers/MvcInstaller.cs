using Microsoft.Extensions.DependencyInjection;

namespace TheXReasonPodcast.Application.Installers
{
    public static class MvcInstaller
    {
        public static IServiceCollection AddMvcServices(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }
    }
}