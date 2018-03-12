using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Models.Responses
{
    public class CalculateMonthlyCostResponse
    {
        public IEnumerable<MonthlyCostItem> MonthlyCosts { get; set; }
    }
}