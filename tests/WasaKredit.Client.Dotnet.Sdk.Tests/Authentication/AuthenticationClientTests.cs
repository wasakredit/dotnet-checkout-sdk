using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http;
using WasaKredit.Client.Dotnet.Sdk.Authentication;
using WasaKredit.Client.Dotnet.Sdk.Exceptions;
using WasaKredit.Client.Dotnet.Sdk.Tests.RestClient;
using Xunit;

namespace WasaKredit.Client.Dotnet.Sdk.Tests.Authentication
{
    public class AuthenticationClientTests : TestBase
    {
        [Theory]
        [InlineData("Invalid client id", "", "secret")]
        [InlineData("Invalid client secret", "id", "")]
        public void expect_argument_exception_when_creating_authentication_client_with_invalid_clientid(string expectedMessage, string clientId, string clientSecret)
        {
            try
            {
                var authenticationClient = AuthenticationClient.Instance;
                authenticationClient.SetClientCredentials(clientId, clientSecret);
            }
            catch (WasaKreditClientException ex)
            {
                Assert.Equal(expectedMessage, ex.Message);
            }
        }

        protected const string FakeAuthenticationErrorJsonResult = @"{
  ""error"": ""invalid client""
}";

        [Fact]
        public async void expect_access_token()
        {
            // Arrange
            HttpClientMock
                .When(HttpMethod.Post, "*")
                .Respond(HttpStatusCode.OK, "application/json", FakeAuthenticationJsonResult);

            var authenticationClient = AuthenticationClient.Instance;
            var client = GetClient();
            authenticationClient.SetClientCredentials(client, "id", "secret");

            // Act
            var response = await authenticationClient.GetAccessTokenAsync();

            // Assert
            Assert.NotNull(response);
        }

        [Fact]
        public async void expect_correct_access_token_token()
        {
            // Arrange
            HttpClientMock
                .When(HttpMethod.Post, "*")
                .Respond(HttpStatusCode.OK, "application/json", FakeAuthenticationJsonResult);

            var authenticationClient = AuthenticationClient.Instance;
            var client = GetClient();
            authenticationClient.SetClientCredentials(client, "id", "secret");

            // Act
            var response = await authenticationClient.GetAccessTokenAsync();

            // Assert
            Assert.Equal("token", response.Token);
        }

        [Fact]
        public async void expect_in_memory_token_when_get_access_token_second_time()
        {
            // Arrange
            HttpClientMock
                .When(HttpMethod.Post, "*")
                .Respond(HttpStatusCode.OK, "application/json", FakeAuthenticationJsonResult);

            var authenticationClient = AuthenticationClient.Instance;
            var client = GetClient();
            authenticationClient.SetClientCredentials(client, "id", "secret");

            // Act
            var response = await authenticationClient.GetAccessTokenAsync();

            HttpClientMock.Clear();

            var secondResponse = await authenticationClient.GetAccessTokenAsync();

            // Assert
            Assert.Equal("token", response.Token);
            Assert.Equal(response.Token, secondResponse.Token);
        }

        [Theory]
        [InlineData(@"{
  ""error"": ""invalid client""
}", HttpStatusCode.Unauthorized)]
        [InlineData(@"{
  ""error"": ""bad request""
}", HttpStatusCode.BadRequest)]
        public async void expect_authentication_exception_with_correct_error_when_non_successfull_request(string expectedMessage, HttpStatusCode expectedStatusCode)
        {
            // Arrange
            HttpClientMock
                .When(HttpMethod.Post, "*")
                .Respond(expectedStatusCode, "application/json", expectedMessage);

            var authenticationClient = AuthenticationClient.Instance;
            var client = GetClient();
            authenticationClient.SetClientCredentials(client, "id", "secret");

            // Act
            try
            {
                var response = await authenticationClient.GetAccessTokenAsync();
            }
            catch (WasaKreditAuthenticationException ex)
            {

                // Assert
                Assert.Equal(expectedStatusCode, ex.StatusCode);
                Assert.Equal(expectedMessage, ex.Message);
            }
        }
    }
}
