using Microsoft.Extensions.DependencyInjection;
using TheXReasonPodcast.Infrastructure.Interfaces;
using TheXReasonPodcast.Infrastructure.Repositories;

namespace TheXReasonPodcast.Application.Installers
{
    public static class RepositoryInstaller
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IEpisodeRepository, EpisodeRepository>();

            return services;
        }
    }
}