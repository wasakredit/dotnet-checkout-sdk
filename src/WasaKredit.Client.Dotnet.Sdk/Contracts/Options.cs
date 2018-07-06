using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts
{
    public class Options
    {
        public int DefaultContractLength { get; set; }

        public IEnumerable<ContractLengthItem> ContractLengths { get; set; }
    }
}