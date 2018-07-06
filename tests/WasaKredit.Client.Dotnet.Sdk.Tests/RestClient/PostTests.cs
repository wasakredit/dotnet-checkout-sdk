using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using WasaKredit.Client.Dotnet.Sdk.Exceptions;
using Xunit;

namespace WasaKredit.Client.Dotnet.Sdk.Tests.RestClient
{
    public class PostTests : TestBase
    {
        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        public async void When_Receiving_PostRequest_Response_With_Correct_Status_Code_Expect_Correct_Deserialized_Result(HttpStatusCode expectedStatusCode)
        {
           
            HttpClientMock
                .When(HttpMethod.Post, "http://localhost/*")
                .Respond(expectedStatusCode, "application/json", FakeJsonResult);

            var restClient = GetClient();

            var expectedResponseObject = new TestObject { StringValue = "Test value", DecimalValue = 3.14m };

            // act
            var restResponse = await restClient.PostAsync<TestObject, TestObject>("http://localhost/", expectedStatusCode, expectedResponseObject);

            // assert
            Assert.Equal(expectedStatusCode, restResponse.StatusCode);
            Assert.Null(restResponse.ErrorString);
            Assert.Equal(restResponse.Result.StringValue, expectedResponseObject.StringValue);
            Assert.Equal(restResponse.Result.DecimalValue, expectedResponseObject.DecimalValue);
        }

        [Theory]
        [InlineData(HttpStatusCode.OK, HttpStatusCode.Created)]
        [InlineData(HttpStatusCode.Created, HttpStatusCode.OK)]
        public async void when_postrequest_response_throws_apiexception_with_error_object(HttpStatusCode expectedStatusCode, HttpStatusCode actualStatusCode)
        {
            // arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = actualStatusCode,
                Content = new StringContent(FakeJsonResult)
            };
            
            HttpClientMock
                .When(HttpMethod.Post, "http://localhost/*")
                .Respond(req => httpResponseMessage);

            var restClient = GetClient();

            var expectedResponseObject = new TestObject { StringValue = "Test value", DecimalValue = 3.14m };

            // act
            try
            {
                var restResponse = await restClient.PostAsync<TestObject, TestObject>("http://localhost/", expectedStatusCode, expectedResponseObject);
            }
            catch (WasaKreditApiException exception)
            {
                //Assert
                Assert.NotNull(exception.Error);
            }
        }

        [Fact]
        public async void when_post_response_throws_api_exception_error_deserilizes_correctly()
        {
            // arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(FakeErrorJsonResult)
            };

            HttpClientMock
                .When(HttpMethod.Post, "http://localhost/*")
                .Respond(req => httpResponseMessage);

            var restClient = GetClient();

            var expectedResponseObject = new TestObject { StringValue = "Test value", DecimalValue = 3.14m };

            // act
            try
            {
                var restResponse = await restClient.PostAsync<TestObject, TestObject>("http://localhost/", HttpStatusCode.OK, expectedResponseObject);
            }
            catch (WasaKreditApiException exception)
            {
                //Assert
                Assert.Equal("resource name", exception.Error.FriendlyResourceName);
                Assert.Equal("RESOURCE_NOT_FOUND", exception.Error.ErrorCode);
                Assert.Equal("developer message", exception.Error.DeveloperMessage);
                Assert.Equal("error description", exception.Error.Description);
                Assert.Equal("id", exception.Error.ResourceId);
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized, "unauthorized")]
        [InlineData(HttpStatusCode.InternalServerError, "internal server error")]
        public async void when_post_request_throws_api_exception_with_non_deseriazable_error_response_map_result_to_developer_description(HttpStatusCode actualStatusCode, string actualResponse)
        {
            // arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = actualStatusCode,
                Content = new StringContent(actualResponse)
            };

            HttpClientMock
                .When(HttpMethod.Post, "http://localhost/*")
                .Respond(req => httpResponseMessage);

            var restClient = GetClient();

            var expectedResponseObject = new TestObject { StringValue = "Test value", DecimalValue = 3.14m };

            // act
            try
            {
                var restResponse = await restClient.PostAsync<TestObject, TestObject>("http://localhost/", HttpStatusCode.OK, expectedResponseObject);
            }
            catch (WasaKreditApiException exception)
            {
                //Assert
                Assert.Equal(actualResponse, exception.Error.DeveloperMessage);
                Assert.Equal(actualStatusCode.ToString(), exception.Error.ErrorCode);
            }
        }

        [Fact]
        public async void when_post_throws_api_exception_expect_correct_correlation_id_in_response()
        {
            // arrange
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            var expectedCorrelationId = Guid.NewGuid().ToString();

            httpResponseMessage.Headers.Add("x-correlation-id", expectedCorrelationId);

            HttpClientMock
                .When(HttpMethod.Post, "http://localhost/*")
                .Respond(req => httpResponseMessage);

            var restClient = GetClient();

            var expectedResponseObject = new TestObject { StringValue = "Test value", DecimalValue = 3.14m };

            // act
            try
            {
                var restResponse = await restClient.PostAsync<TestObject, TestObject>("http://localhost/", HttpStatusCode.OK, expectedResponseObject);
            }
            catch (WasaKreditApiException exception)
            {
                //Assert
                Assert.Equal(expectedCorrelationId, exception.CorrelationId.ToString());
            }
        }
    }
}
