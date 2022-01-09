namespace TheXReasonPodcast.Application.Models.Responses
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}