using System;
using System.Collections.Generic;
using System.Text;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Requests
{
    public class CancelOrderRequest
    {
        public Guid OrderId { get; set; }
    }
}
