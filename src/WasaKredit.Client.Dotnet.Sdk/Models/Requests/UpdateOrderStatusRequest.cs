using System;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Requests
{
    public class UpdateOrderStatusRequest
    {
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}