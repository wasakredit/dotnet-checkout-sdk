using Newtonsoft.Json;

namespace WasaKredit.Client.Dotnet.Sdk.Exceptions
{
    public class WasaKreditAuthenticationError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
