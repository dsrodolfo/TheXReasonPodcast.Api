namespace TheXReasonPodcast.Application.Models.Requests
{
    public class RefreshCredentialsRequest
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}