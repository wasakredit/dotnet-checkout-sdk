using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WasaKredit.Client.Dotnet.Sdk.Models;
using WasaKredit.Client.Dotnet.Sdk.RestClient;

namespace WasaKredit.Client.Dotnet.Sdk.Authentication
{
    /// <summary>
    /// A singleton class representing the authentication client used to authenticate against WasaKredit Checkout Gateway API. The singleton instance is accessed through the "Instance" property.
    /// </summary>
    public sealed class AuthenticationClient : IAuthenticationClient
    {
        private static AuthenticationClient _instance;
        private static IRestClient _restClient;
        private static string _clientId;
        private static string _clientSecret;
        private static readonly string _authenticationUrl = "https://b2b.services.wasakredit.se/auth/connect/token";

        private AccessToken _accessToken;

        /// <summary>
        /// Accesses the singleton instance of AuthenticationClient used to authenticate against WasaKredit Checkout Gateway API.
        /// </summary>
        public static AuthenticationClient Instance
        {
            get { return _instance ?? (_instance = new AuthenticationClient()); }
        }

        private AuthenticationClient() {}

        /// <summary>
        /// Sets client credentials for the authentication client singleton instance.
        /// </summary>
        /// <param name="clientId">Your client id provided by WasaKredit</param>
        /// <param name="clientSecret">Your client secret provided by WasaKredit</param>
        public void SetClientCredentials(string clientId, string clientSecret)
        {
            SetClientCredentials(new RestClient.RestClient(TimeSpan.FromSeconds(10), false), clientId, clientSecret);
        }

        internal void SetClientCredentials(IRestClient restClient, string clientId, string clientSecret)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new WasaKreditClientException("Invalid client id");
            }

            if (string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new WasaKreditClientException("Invalid client secret");
            }

            _restClient = restClient;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<AccessToken> GetAccessTokenAsync()
        {
            CheckInitialized();

            if(_accessToken != null && _accessToken.Valid)
            {
                return _accessToken;
            }

            var authorizationContent = CreateAuthenticationContent();

            var response = await _restClient.PostUrlEncodedAsync<AuthenticationResponse>(_authenticationUrl, System.Net.HttpStatusCode.OK, authorizationContent);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new WasaKreditAuthenticationException(response.ErrorString, response.StatusCode, response.CorrelationId);
            }

            SetAccessToken(response.Result);

            return _accessToken;
        }

        public AccessToken GetAccessToken()
        {
            CheckInitialized();

            if (_accessToken != null && _accessToken.Valid)
            {
                return _accessToken;
            }

            var authorizationContent = CreateAuthenticationContent();

            var response = _restClient.PostUrlEncoded<AuthenticationResponse>(_authenticationUrl, System.Net.HttpStatusCode.OK, authorizationContent);
            
            SetAccessToken(response.Result);

            return _accessToken;
        }

        private void SetAccessToken(AuthenticationResponse response)
        {
            int expiresInSeconds;

            if (!int.TryParse(response.ExpiresIn, out expiresInSeconds))
            {
                throw new WasaKreditClientException("Error parsing expires_in value from authorize response");
            }

             _accessToken = new AccessToken(response.AccessToken, DateTime.UtcNow.AddMinutes(expiresInSeconds));
        }

        private IEnumerable<KeyValuePair<string, string>> CreateAuthenticationContent()
        {
            CheckInitialized();

            var clientIdKeyValuePair = new KeyValuePair<string, string>("client_id", _clientId);
            var clientSecretKeyValuePair = new KeyValuePair<string, string>("client_secret", _clientSecret);
            var grantTypeKeyValuePair = new KeyValuePair<string, string>("grant_type", "client_credentials");

            var formUrlEncodedHeaders = new List<KeyValuePair<string, string>>();

            formUrlEncodedHeaders.Add(clientIdKeyValuePair);
            formUrlEncodedHeaders.Add(clientSecretKeyValuePair);
            formUrlEncodedHeaders.Add(grantTypeKeyValuePair);

            return formUrlEncodedHeaders;
        }

        private void CheckInitialized()
        {
            if (_restClient == null || string.IsNullOrEmpty(_clientId) || string.IsNullOrEmpty(_clientSecret))
            {
                throw new WasaKreditClientException("No client credentials has been set for the authentication client.");
            }
        }
    }
}