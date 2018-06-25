using WasaKredit.Client.Dotnet.Sdk.Contracts;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Requests
{
    public class CalculateTotalLeasingCostRequest
    {
        public Price TotalAmount { get; set; }
    }
}
