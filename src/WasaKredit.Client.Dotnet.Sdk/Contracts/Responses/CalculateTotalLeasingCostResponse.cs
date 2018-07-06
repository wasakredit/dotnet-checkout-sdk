using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Responses
{
    public class CalculateTotalLeasingCostResponse
    {
        public int DefaultContractLength { get; set; }

        public IEnumerable<ContractLengthObject> ContractLengths { get; set; }
    }
}
