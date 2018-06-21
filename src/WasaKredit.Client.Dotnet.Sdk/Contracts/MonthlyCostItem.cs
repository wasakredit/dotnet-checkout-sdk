using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts
{
    public class MonthlyCostItem
    {
        public Price MonthlyCost { get; set; }

        public string ProductId { get; set; }
    }
}