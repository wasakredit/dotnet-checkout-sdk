using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Contracts;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Responses
{
    public class CalculateTotalLeasingCostResponse
    {
        public int DefaultContractLength { get; set; }

        public IEnumerable<ContractLengthObject> ContractLengths { get; set; }
    }
}
