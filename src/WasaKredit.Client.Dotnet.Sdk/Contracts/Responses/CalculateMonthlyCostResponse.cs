using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Responses
{
    public class CalculateMonthlyCostResponse
    {
        public IEnumerable<MonthlyCostItem> MonthlyCosts { get; set; }
    }
}