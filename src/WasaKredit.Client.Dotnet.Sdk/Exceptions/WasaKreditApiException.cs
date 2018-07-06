using System;
using System.Net;

namespace WasaKredit.Client.Dotnet.Sdk.Exceptions
{
    public class WasaKreditApiException : WebException
    {
        public WasaKreditApiException(
            WasaKreditError error,
            Guid correlationId)
            :base(error.DeveloperMessage)
        {
            Error = error;
            CorrelationId = correlationId;
        }

        public WasaKreditError Error { get; private set; }

        public Guid CorrelationId { get; private set; }
    }
}
