namespace WasaKredit.Client.Dotnet.Sdk
{
    public static class Settings
    {
        public const string AUTHENTICATION_URL = "https://b2b.services.wasakredit.se/auth/connect/token";
        public const string CHECKOUT_GATEWAY_API_URL = "https://b2b.services.wasakredit.se";

        public const string TEST_AUTHENTICATION_URL = "http://auth.internalsvc-test.wasakredit.se:8080/connect/token";
        public const string TEST_CHECKOUT_GATEWAY_API_URL = "http://internalsvc-test.wasakredit.se:8080/api";
    }
}
