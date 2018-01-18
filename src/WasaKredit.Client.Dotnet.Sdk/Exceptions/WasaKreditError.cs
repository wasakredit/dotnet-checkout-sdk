using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Exceptions;

namespace WasaKredit.Client.Dotnet.Sdk.Models
{
    public class WasaKreditError
    {
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        [JsonProperty("developer_message")]
        public string DeveloperMessage { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("resource_id")]
        public string ResourceId { get; private set; }

        [JsonProperty("friendly_resource_name")]
        public string FriendlyResourceName { get; set; }

        [JsonProperty("invalid_properties")]
        public IEnumerable<InvalidProperty> InvalidProperties { get; set; }
    }
}
