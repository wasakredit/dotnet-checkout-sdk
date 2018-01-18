using System;

namespace WasaKredit.Client.Dotnet.Sdk.Models
{
    public class WasaKreditClientException : ArgumentException
    {
        public WasaKreditClientException(string message)
            :base(message)
        {

        }
    }
}
