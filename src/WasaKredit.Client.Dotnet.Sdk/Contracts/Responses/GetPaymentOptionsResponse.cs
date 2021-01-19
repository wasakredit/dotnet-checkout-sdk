using System;
using System.Collections.Generic;
using System.Text;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Responses
{
    public class GetPaymentOptionsResponse
    {
        public int DefaultContractLength { get; set; }

        public IEnumerable<ContractLengthItem> ContractLengths { get; set; }
    }

}
