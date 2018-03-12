using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Models.Requests
{
    public class CalculateMonthlyCostRequest
    {
        public IEnumerable<Item> Items { get; set; }
    }
}