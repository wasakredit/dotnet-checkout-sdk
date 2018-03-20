using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Responses
{
    public class AddOrderReferenceResponse
    {
        public IEnumerable<OrderReference> OrderReferences { get; set; }
    }
}