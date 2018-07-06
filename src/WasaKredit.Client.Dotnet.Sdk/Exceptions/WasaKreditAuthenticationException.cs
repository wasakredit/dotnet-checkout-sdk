using System;
using System.Net;

namespace WasaKredit.Client.Dotnet.Sdk.Exceptions
{
    public class WasaKreditAuthenticationException : Exception
    {
        public WasaKreditAuthenticationException(string message, HttpStatusCode statusCode, Guid correlationId)
            : base (message)
        {
            StatusCode = statusCode;
            CorrelationId = correlationId;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public Guid CorrelationId { get; private set; }
    }
}
