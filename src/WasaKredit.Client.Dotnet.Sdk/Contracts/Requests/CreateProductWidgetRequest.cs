namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Requests
{
    public class CreateProductWidgetRequest
    {
        public string FinancialProduct { get; set; }

        public Price PriceExVat { get; set; }
    }
}
