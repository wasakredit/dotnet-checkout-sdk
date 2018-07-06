using System;

namespace WasaKredit.Client.Dotnet.Sdk.Exceptions
{
    public class WasaKreditClientException : ArgumentException
    {
        public WasaKreditClientException(string message)
            :base(message)
        {

        }
    }
}
