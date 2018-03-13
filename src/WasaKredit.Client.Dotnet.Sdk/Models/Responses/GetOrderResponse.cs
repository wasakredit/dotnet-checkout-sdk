using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Responses
{
    public class GetOrderResponse
    {
        public string CustomerOrganizationNumber { get; set; }
        public Address BillingAddress { get; set; }
        public Address DeliveryAddress { get; set; }
        public IEnumerable<OrderReference> OrderReferences { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public OrderStatus Status { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}