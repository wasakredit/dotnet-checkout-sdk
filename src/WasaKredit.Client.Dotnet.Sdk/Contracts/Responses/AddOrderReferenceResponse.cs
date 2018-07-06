using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Responses
{
    public class AddOrderReferenceResponse
    {
        public IEnumerable<OrderReference> OrderReferences { get; set; }
    }
}