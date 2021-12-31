using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TheXReasonPodcast.Application.Mappings;

namespace TheXReasonPodcast.Application.Installers
{
    public static class AutoMapperInstaller
    {
        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EpisodeProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}