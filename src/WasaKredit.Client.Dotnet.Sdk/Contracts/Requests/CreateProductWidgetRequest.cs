using WasaKredit.Client.Dotnet.Sdk.Contracts;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Requests
{
    public class CreateProductWidgetRequest
    {
        public string FinancialProduct { get; set; }

        public Price PriceExVat { get; set; }
    }
}
