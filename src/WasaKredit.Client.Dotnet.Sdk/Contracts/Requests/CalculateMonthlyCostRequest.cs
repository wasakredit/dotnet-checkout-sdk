using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Contracts;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Requests
{
    public class CalculateMonthlyCostRequest
    {
        public IEnumerable<Item> Items { get; set; }
    }
}