using System.Threading.Tasks;

namespace WasaKredit.Client.Dotnet.Sdk.Authentication
{
    public interface IAuthenticationClient
    {
        Task<AccessToken> GetAccessTokenAsync();

        AccessToken GetAccessToken();

        void SetTestMode(bool isTestMode);
    }
}
