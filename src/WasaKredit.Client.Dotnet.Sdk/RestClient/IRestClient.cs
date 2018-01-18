using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WasaKredit.Client.Dotnet.Sdk.RestClient
{
    internal interface IRestClient
    {
        WasaKreditResponse<TResponse> Get<TResponse>(string uri, HttpStatusCode expectedStatusCode, string authorizationToken = null, string authorizationMethod = "Bearer");

        Task<WasaKreditResponse<TResponse>> GetAsync<TResponse>(string uri, HttpStatusCode expectedStatusCode, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken));

        WasaKreditResponse<TResponse> Delete<TResponse>(string uri, HttpStatusCode expectedStatusCode, string authorizationToken = null, string authorizationMethod = "Bearer");

        Task<WasaKreditResponse<TResponse>> DeleteAsync<TResponse>(string uri, HttpStatusCode expectedStatusCode, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken));

        WasaKreditResponse<TResponse> Post<TRequest, TResponse>(string uri, HttpStatusCode expectedStatusCode, TRequest contract, string authorizationToken = null, string authorizationMethod = "Bearer");

        Task<WasaKreditResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, HttpStatusCode expectedStatusCode, TRequest contract, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken));

        WasaKreditResponse<TResponse> Put<TRequest, TResponse>(string uri, HttpStatusCode expectedStatusCode, TRequest contract, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken));

        Task<WasaKreditResponse<TResponse>> PutAsync<TRequest, TResponse>(string uri, HttpStatusCode expectedStatusCode, TRequest contract, string authorizationToken = null, string authorizationMethod = "Bearer", CancellationToken cancellationToken = default(CancellationToken));

        Task<WasaKreditResponse<TResponse>> PostUrlEncodedAsync<TResponse>(string uri, HttpStatusCode expectedStatusCode, IEnumerable<KeyValuePair<string, string>> formUrlEncodedContent, CancellationToken cancellationToken = default(CancellationToken));

        WasaKreditResponse<TResponse> PostUrlEncoded<TResponse>(string uri, HttpStatusCode expectedStatusCode, IEnumerable<KeyValuePair<string, string>> formUrlEncodedContent);

    }
}