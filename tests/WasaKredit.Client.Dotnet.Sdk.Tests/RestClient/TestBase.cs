using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RichardSzalay.MockHttp;

namespace WasaKredit.Client.Dotnet.Sdk.Tests.RestClient
{
    public abstract class TestBase
    {
        protected const string FakeJsonResult = @"{
    ""string_value"": ""Test value"",
    ""decimal_value"": 3.14
}";
        protected const string FakeErrorInvalidPropertiesJsonResult = @"{
  ""error_code"": ""REQUEST_VALIDATION_ERROR"",
  ""developer_message"": ""The request has some invalid properties."",
  ""description"": ""The request has some invalid properties."",
  ""invalid_properties"": [
    {
      ""property"": ""<property name>"",
      ""error_message"": ""<message>""
    }
  ]
}";
        protected const string FakeErrorJsonResult = @"{
  ""error_code"": ""RESOURCE_NOT_FOUND"",
  ""developer_message"": ""developer message"",
  ""description"": ""error description"",
  ""resource_id"": ""id"",
  ""friendly_resource_name"": ""resource name""
}";

        protected const string FakeAuthenticationJsonResult = @"{
  ""access_token"": ""token"",
  ""expires_in"": 3600,
  ""token_type"": ""Bearer""
}";

        public MockHttpMessageHandler HttpClientMock { get; private set; }

        public TestBase()
        {
            HttpClientMock = new MockHttpMessageHandler();
        }

        internal Sdk.RestClient.RestClient GetClient()
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            return new Sdk.RestClient.RestClient(HttpClientMock.ToHttpClient(), jsonSerializerSettings, true);
        }
    }
}
