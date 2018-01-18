using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace WasaKredit.Client.Dotnet.Sdk.Tests.RestClient
{
    public class GetTests : TestBase
    {
        [Theory]
        [InlineData(HttpStatusCode.OK, HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created, HttpStatusCode.Created)]
        public async void when_receiving_postrequest_response_with_correct_status_code_expect_correct_deserilized_result(HttpStatusCode expectedStatusCode, HttpStatusCode actualStatusCode)
        {
            // Arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(FakeJsonResult, Encoding.UTF8, "application/json")
            };

            HttpClientMock
                .When(HttpMethod.Get, "http://localhost/*")
                .Respond(expectedStatusCode, "application/json", FakeJsonResult);

            var restClient = GetClient();

            var expectedResponseObject = new TestObject { StringValue = "Test value", DecimalValue = 3.14m };

            // act
            var restResponse = await restClient.GetAsync<TestObject>("http://localhost/", expectedStatusCode);

            // assert
            Assert.Equal(expectedStatusCode, restResponse.StatusCode);
            Assert.Null(restResponse.ErrorString);
            Assert.Equal(restResponse.Result.StringValue, expectedResponseObject.StringValue);
            Assert.Equal(restResponse.Result.DecimalValue, expectedResponseObject.DecimalValue);
        }
    }
}
