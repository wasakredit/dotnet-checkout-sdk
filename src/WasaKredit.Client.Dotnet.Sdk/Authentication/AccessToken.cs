using System;

namespace WasaKredit.Client.Dotnet.Sdk.Authentication
{
    public class AccessToken
    {
        public AccessToken(string accessToken, DateTime expires)
        {
            Token = accessToken;
            Expires = expires;
        }

        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public bool Valid { get { return DateTime.UtcNow.AddSeconds(10) < Expires; } }
    }
}