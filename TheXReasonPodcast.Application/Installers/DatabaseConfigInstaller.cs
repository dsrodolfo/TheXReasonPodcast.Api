using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TheXReasonPodcast.Infrastructure.Configurations;
using TheXReasonPodcast.Infrastructure.Interfaces;

namespace TheXReasonPodcast.Application.Installers
{
    public static class DatabaseConfigInstaller
    {
        public static IServiceCollection AddDatabaseConfigServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseConfig>(configuration.GetSection(nameof(DatabaseConfig)));
            services.AddSingleton<IDatabaseConfig>(sp => sp.GetRequiredService<IOptions<DatabaseConfig>>().Value);

            return services;
        }
    }
}