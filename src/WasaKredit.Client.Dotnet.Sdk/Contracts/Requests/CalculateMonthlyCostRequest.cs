using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Requests
{
    public class CalculateMonthlyCostRequest
    {
        public IEnumerable<Item> Items { get; set; }
    }
}