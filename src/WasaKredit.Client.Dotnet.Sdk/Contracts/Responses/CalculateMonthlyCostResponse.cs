using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Contracts;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Responses
{
    public class CalculateMonthlyCostResponse
    {
        public IEnumerable<MonthlyCostItem> MonthlyCosts { get; set; }
    }
}