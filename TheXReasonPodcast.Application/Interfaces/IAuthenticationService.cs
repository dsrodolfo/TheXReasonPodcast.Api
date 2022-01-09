using TheXReasonPodcast.Application.Models.Requests;
using TheXReasonPodcast.Application.Models.Responses;

namespace TheXReasonPodcast.Application.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest authenticateRequest);
        AuthenticateResponse RefreshCredentials(RefreshCredentialsRequest refreshCredentialsRequest);
    }
}