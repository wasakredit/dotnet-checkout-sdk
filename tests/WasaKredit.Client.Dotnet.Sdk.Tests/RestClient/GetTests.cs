using RichardSzalay.MockHttp;
using System.Net;
using System.Net.Http;
using Xunit;

namespace WasaKredit.Client.Dotnet.Sdk.Tests.RestClient
{
    public class GetTests : TestBase
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        public async void When_Receiving_GetRequest_Response_With_Correct_Status_Code_Expect_Correct_Deserialized_Result(HttpStatusCode expectedStatusCode)
        {
            // Arrange
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
