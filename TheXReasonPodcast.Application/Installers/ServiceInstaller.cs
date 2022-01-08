using Microsoft.Extensions.DependencyInjection;
using TheXReasonPodcast.Application.Interfaces;
using TheXReasonPodcast.Application.Services;

namespace TheXReasonPodcast.Application.Installers
{
    public static class ServiceInstaller
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IEpisodeService, EpisodeService>();

            return services;
        }
    }
}