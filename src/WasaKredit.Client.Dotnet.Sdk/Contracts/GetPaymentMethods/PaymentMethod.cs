namespace WasaKredit.Client.Dotnet.Sdk.Contracts.GetPaymentMethods
{
    public class PaymentMethod
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public Options Options { get; set; }
    }
}