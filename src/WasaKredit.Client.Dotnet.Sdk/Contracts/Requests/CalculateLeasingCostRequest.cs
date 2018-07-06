using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Requests
{
    public class CalculateLeasingCostRequest
    {
        public IEnumerable<Item> Items { get; set; }
    }
}
