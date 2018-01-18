using Newtonsoft.Json;

namespace WasaKredit.Client.Dotnet.Sdk.Exceptions
{
    public class InvalidProperty
    {
        [JsonProperty("property")]
        public string Property { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
