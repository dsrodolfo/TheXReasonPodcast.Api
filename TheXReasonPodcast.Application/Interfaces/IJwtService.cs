namespace TheXReasonPodcast.Application.Interfaces
{
    public interface IJwtService
    {
        string Login(string username, string password);
    }
}