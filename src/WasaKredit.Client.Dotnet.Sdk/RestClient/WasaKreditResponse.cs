using System;
using System.Net;

namespace WasaKredit.Client.Dotnet.Sdk.RestClient
{
    public class WasaKreditResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public Guid CorrelationId { get; set; }

        public T Result { get; set; }

        public string ResultString { get; set; }
        public string ErrorString { get; set; }
        public string LocationHeaderValue { get; set; }
    }
}