namespace TheXReasonPodcast.Infrastructure.Interfaces
{
    public interface IDatabaseConfig
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}