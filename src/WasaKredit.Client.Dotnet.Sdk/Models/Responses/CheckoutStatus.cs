using System;
using System.Collections.Generic;
using System.Text;

namespace WasaKredit.Client.Dotnet.Sdk.Responses
{
    public class CheckoutStatus
    {
        public string Id { get; set; }

        public string Status { get; set; }

        public string OrderReferenceId { get; set; }
    }
}
