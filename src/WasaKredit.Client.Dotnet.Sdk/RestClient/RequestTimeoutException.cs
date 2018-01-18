using System.Runtime.Serialization;

namespace WasaKredit.Client.Dotnet.Sdk.RestClient
{
    internal class RequestTimeoutException : System.Exception
    {
        private string _requestUri;
        private int _timeoutInSeconds;

        public RequestTimeoutException(string requestUri, int timeoutInSeconds)
        {
            _requestUri = requestUri;
            _timeoutInSeconds = timeoutInSeconds;
        }

        protected RequestTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _timeoutInSeconds = info.GetInt32("TimeoutInSeconds");
            _requestUri = info.GetString("RequestUri");
        }
    }
}