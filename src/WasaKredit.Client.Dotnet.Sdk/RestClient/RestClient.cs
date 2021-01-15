using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WasaKredit.Client.Dotnet.Sdk.Exceptions;
using System.Threading;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WasaKredit.Client.Dotnet.Sdk.Tests")]

namespace WasaKredit.Client.Dotnet.Sdk.RestClient
{
    internal class RestClient : IRestClient
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly TimeSpan _timeout;
        private readonly bool _testMode;

        internal RestClient(TimeSpan timeout, bool testMode)
            : this(
                new HttpClient(),
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                },
                testMode)
        {
            _timeout = timeout;
            _client.Timeout = timeout;
            _testMode = testMode;
        }

        internal RestClient(HttpClient client, JsonSerializerSettings serializerSettings, bool testMode)
        {
            _client = client;
            _serializerSettings = serializerSettings;
        }

     

        public WasaKreditResponse<TResponse> Get<TResponse>(string uri, HttpStatusCode expectedStatusCode, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var responseMessage = DoGetDelete(uri, HttpMethod.Get, authorizationToken, authorizationMethod).Result;

            var response = GetWasaKreditResponse<TResponse>(expectedStatusCode, responseMessage).Result;

            if (response.StatusCode == expectedStatusCode)
                return response;

            throw CreateWasaKreditApiException(response);
        }

        public async Task<WasaKreditResponse<TResponse>> GetAsync<TResponse>(string uri, HttpStatusCode expectedStatusCode, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken))
        {
            var responseMessage = await DoGetDeleteAsync(uri, HttpMethod.Get, authorizationToken, authorizationMethod);

            return await GetWasaKreditResponseAsync<TResponse>(expectedStatusCode, responseMessage);
        }

        public WasaKreditResponse<TResponse> Delete<TResponse>(string uri, HttpStatusCode expectedStatusCode, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var responseMessage = DoGetDelete(uri, HttpMethod.Delete, authorizationToken, authorizationMethod).Result;

            var response = GetWasaKreditResponse<TResponse>(expectedStatusCode, responseMessage).Result;

            if (response.StatusCode == expectedStatusCode)
                return response;

            throw CreateWasaKreditApiException(response);
        }

        public async Task<WasaKreditResponse<TResponse>> DeleteAsync<TResponse>(string uri, HttpStatusCode expectedStatusCode, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken))
        {
            var responseMessage = await DoGetDeleteAsync(uri, HttpMethod.Delete, authorizationToken, authorizationMethod);

            var response = await GetWasaKreditResponseAsync<TResponse>(expectedStatusCode, responseMessage);

            if (response.StatusCode == expectedStatusCode)
                return response;

            throw CreateWasaKreditApiException(response);
        }

        public WasaKreditResponse<TResponse> Post<TRequest, TResponse>(string uri, HttpStatusCode expectedStatusCode, TRequest contract, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var responseMessage = DoPostPut(HttpMethod.Post, uri, contract, authorizationToken, authorizationMethod).Result;

            var response = GetWasaKreditResponse<TResponse>(expectedStatusCode, responseMessage).Result;

            if (response.StatusCode == expectedStatusCode)
                return response;

            throw CreateWasaKreditApiException(response);
        }

        public async Task<WasaKreditResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, HttpStatusCode expectedStatusCode, TRequest contract, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken))
        {
            var responseMessage = await DoPostPutAsync(HttpMethod.Post, uri, contract, authorizationToken, authorizationMethod);
            
            var response = await GetWasaKreditResponseAsync<TResponse>(expectedStatusCode, responseMessage);

            if (response.StatusCode == expectedStatusCode)
                return response;

            throw CreateWasaKreditApiException(response);
        }

        public WasaKreditResponse<TResponse> Put<TRequest, TResponse>(string uri, HttpStatusCode expectedStatusCode, TRequest contract,
            string authorizationToken = null, string authorizationMethod = "Bearer",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var responseMessage = DoPostPut(HttpMethod.Put, uri, contract, authorizationToken, authorizationMethod).Result;

            var response = GetWasaKreditResponse<TResponse>(expectedStatusCode, responseMessage).Result;

            if (response.StatusCode == expectedStatusCode)
                return response;

            throw CreateWasaKreditApiException(response);
        }

        public async Task<WasaKreditResponse<TResponse>> PutAsync<TRequest, TResponse>(string uri, HttpStatusCode expectedStatusCode, TRequest contract, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken))
        {
            var responseMessage = await DoPostPutAsync(HttpMethod.Put, uri, contract, authorizationToken, authorizationMethod);

            return await GetWasaKreditResponseAsync<TResponse>(expectedStatusCode, responseMessage);
        }

        public WasaKreditResponse<TResponse> PostUrlEncoded<TResponse>(string uri, HttpStatusCode expectedStatusCode, IEnumerable<KeyValuePair<string, string>> formUrlEncodedContent)
        {
            var responseMessage = DoPostFormUrlEncoded(HttpMethod.Post, uri, formUrlEncodedContent).Result;

            var response = GetWasaKreditResponse<TResponse>(expectedStatusCode, responseMessage).Result;

            if (response.StatusCode == expectedStatusCode)
                return response;

            throw CreateWasaKreditAuthenticationException(response);
        }

        public async Task<WasaKreditResponse<TResponse>> PostUrlEncodedAsync<TResponse>(string uri, HttpStatusCode expectedStatusCode, IEnumerable<KeyValuePair<string, string>> formUrlEncodedContent, CancellationToken cancellationToken = default(CancellationToken))
        {
            var responseMessage = await DoPostFormUrlEncodedAsync(HttpMethod.Post, uri, formUrlEncodedContent);

            return await GetWasaKreditResponseAsync<TResponse>(expectedStatusCode, responseMessage);
        }

        private async Task<HttpResponseMessage> DoGetDelete(string uri, HttpMethod method, string authorizationToken, string authorizationMethod)
        {
            if (method != HttpMethod.Get && method != HttpMethod.Delete)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            var requestMessage = new HttpRequestMessage(method, uri);

            AddAuthorizationHeader(requestMessage, authorizationToken, authorizationMethod);
            AddTestModeHeader(requestMessage);

            var responseMessage = await _client.SendAsync(requestMessage).ConfigureAwait(false);

            if (responseMessage.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new RequestTimeoutException(uri, (int)_timeout.TotalSeconds);
            }

            return responseMessage;
        }

        private async Task<HttpResponseMessage> DoGetDeleteAsync(string uri, HttpMethod method, string authorizationToken, string authorizationMethod, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (method != HttpMethod.Get && method != HttpMethod.Delete)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }
            
            var requestMessage = new HttpRequestMessage(method, uri);

            AddAuthorizationHeader(requestMessage, authorizationToken, authorizationMethod);
            AddTestModeHeader(requestMessage);

            var responseMessage = await _client.SendAsync(requestMessage, cancellationToken);
            
            if (responseMessage.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new RequestTimeoutException(uri, (int)_timeout.TotalSeconds);
            }

            return responseMessage;
        }

        private async Task<HttpResponseMessage> DoPostPut<T>(HttpMethod method, string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item, _serializerSettings), Encoding.UTF8, "application/json")
            };

            AddAuthorizationHeader(requestMessage, authorizationToken, authorizationMethod);
            AddTestModeHeader(requestMessage);

            var response = await _client.SendAsync(requestMessage).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new RequestTimeoutException(uri, (int)_timeout.TotalSeconds);
            }

            return response;
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken))
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }
            
            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(item, _serializerSettings), Encoding.UTF8, "application/json")
            };

            AddAuthorizationHeader(requestMessage, authorizationToken, authorizationMethod);
            AddTestModeHeader(requestMessage);

            var response = await _client.SendAsync(requestMessage, cancellationToken);
            
            if (response.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new RequestTimeoutException(uri, (int)_timeout.TotalSeconds);
            }

            return response;
        }

        private async Task<HttpResponseMessage> DoPostFormUrlEncoded(HttpMethod method, string uri, IEnumerable<KeyValuePair<string, string>> formUrlEncodedContent)
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new FormUrlEncodedContent(formUrlEncodedContent)
            };

            AddTestModeHeader(requestMessage);

            var response = await _client.SendAsync(requestMessage).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new RequestTimeoutException(uri, (int)_timeout.TotalSeconds);
            }

            return response;
        }

        private async Task<HttpResponseMessage> DoPostFormUrlEncodedAsync(HttpMethod method, string uri, IEnumerable<KeyValuePair<string, string>> formUrlEncodedContent, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }

            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new FormUrlEncodedContent(formUrlEncodedContent)
            };

            AddTestModeHeader(requestMessage);

            var response = await _client.SendAsync(requestMessage, cancellationToken);
            
            if (response.StatusCode == HttpStatusCode.RequestTimeout)
            {
                throw new RequestTimeoutException(uri, (int)_timeout.TotalSeconds);
            }

            return response;
        }

        private async Task<WasaKreditResponse<TResponse>> GetWasaKreditResponse<TResponse>(HttpStatusCode expectedStatusCode, HttpResponseMessage responseMessage)
        {
            var resultString = responseMessage.Content != null
                ? await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false)
                : string.Empty;

            var locationHeaderValue = string.Empty;
            try
            {
                locationHeaderValue = responseMessage
                    .Headers
                    .GetValues("location")
                    .Single();
            }
            catch { }

            var correlationHeaderValue = Guid.Empty;
            try
            {
                correlationHeaderValue = new Guid(responseMessage
                    .Headers
                    .GetValues("x-correlation-id")
                    .Single());
            }
            catch { }

            if (responseMessage.StatusCode != expectedStatusCode)
            {
                return new WasaKreditResponse<TResponse>
                {
                    StatusCode = responseMessage.StatusCode,
                    ErrorString = resultString,
                    LocationHeaderValue = locationHeaderValue,
                    CorrelationId = correlationHeaderValue
                };
            }

            var contentType = responseMessage.Content.Headers.ContentType;

            return new WasaKreditResponse<TResponse>
            {
                StatusCode = responseMessage.StatusCode,
                Result = contentType.MediaType.Contains("application/json") ? JsonConvert.DeserializeObject<TResponse>(resultString, _serializerSettings) : default(TResponse),
                ResultString = resultString,
                LocationHeaderValue = locationHeaderValue,
                CorrelationId = correlationHeaderValue
            };
        }

        private async Task<WasaKreditResponse<TResponse>> GetWasaKreditResponseAsync<TResponse>(HttpStatusCode expectedStatusCode, HttpResponseMessage responseMessage)
        {
            var resultString = responseMessage.Content != null
                ? await responseMessage.Content?.ReadAsStringAsync()
                : string.Empty;

            var locationHeaderValue = string.Empty;
            try
            {
                locationHeaderValue = responseMessage
                    .Headers
                    .GetValues("location")
                    .Single();
            }
            catch {}

            var correlationHeaderValue = Guid.Empty;
            try
            {
                correlationHeaderValue = new Guid(responseMessage
                    .Headers
                    .GetValues("x-correlation-id")
                    .Single());
            }
            catch { }

            if (responseMessage.StatusCode != expectedStatusCode)
            {
                return new WasaKreditResponse<TResponse>
                {
                    StatusCode = responseMessage.StatusCode,
                    ErrorString = resultString,
                    LocationHeaderValue = locationHeaderValue,
                    CorrelationId = correlationHeaderValue
                };
            }
            
            var contentType = responseMessage.Content.Headers.ContentType;

            return new WasaKreditResponse<TResponse>
            {
                StatusCode = responseMessage.StatusCode,
                Result = contentType.MediaType.Contains("application/json") ? JsonConvert.DeserializeObject<TResponse>(resultString, _serializerSettings) : default(TResponse),
                ResultString = resultString,
                LocationHeaderValue = locationHeaderValue,
                CorrelationId = correlationHeaderValue
            };
        }

        private static void AddAuthorizationHeader(HttpRequestMessage request, string authorizationToken, string authorizationMethod)
        {
            if (!string.IsNullOrWhiteSpace(authorizationMethod) && !string.IsNullOrWhiteSpace(authorizationToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
        }

        private void AddTestModeHeader(HttpRequestMessage request)
        {
            request.Headers.Add("x-test-mode", _testMode.ToString());
        }

        private WasaKreditApiException CreateWasaKreditApiException<TResponse>(WasaKreditResponse<TResponse> response)
        {
            WasaKreditError error = new WasaKreditError
            {
                ErrorCode = response.StatusCode.ToString()
            };
            
            if(!string.IsNullOrWhiteSpace(response.ErrorString))
            {
                try
                {
                    error = JsonConvert.DeserializeObject<WasaKreditError>(response.ErrorString, _serializerSettings);
                }
                catch
                {
                    error.DeveloperMessage = response.ErrorString;
                }
            }

            return new WasaKreditApiException(error, response.CorrelationId);
        }

        private WasaKreditAuthenticationException CreateWasaKreditAuthenticationException<TResponse>(WasaKreditResponse<TResponse> response)
        {
            var error = JsonConvert.DeserializeObject<WasaKreditAuthenticationError>(response.ErrorString, _serializerSettings);

            return new WasaKreditAuthenticationException(error.Error, response.StatusCode, response.CorrelationId);
        }
    }
}