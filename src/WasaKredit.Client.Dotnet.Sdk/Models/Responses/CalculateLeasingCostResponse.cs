using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Models;


namespace WasaKredit.Client.Dotnet.Sdk.Responses
{
    public class CalculateLeasingCostResponse
    {
        public IEnumerable<LeasingCost> LeasingCosts { get; set; }
    }
}
