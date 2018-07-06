using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Responses
{
    public class CalculateLeasingCostResponse
    {
        public IEnumerable<LeasingCost> LeasingCosts { get; set; }
    }
}
