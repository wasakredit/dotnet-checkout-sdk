using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Contracts;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace WasaKredit.Client.Dotnet.Sdk.Requests
{
    public class CreateCheckoutRequest
    {
        public string PaymentTypes { get; set; }

        public string OrderReferenceId { get; set; }
        public IEnumerable<OrderReference> OrderReferences { get; set; }

        public IEnumerable<CartItem> CartItems { get; set; }

        public Price ShippingCostExVat { get; set; }

        public string CustomerOrganizationNumber { get; set; }

        public string PurchaserName { get; set; }

        public string PurchaserEmail { get; set; }

        public string PurchaserPhone { get; set; }

        public Address BillingAddress { get; set; }

        public Address DeliveryAddress { get; set; }

        public string RecipientName { get; set; }

        public string RecipientPhone { get; set; }

        public string RequestDomain { get; set; }

        public string ConfirmationCallbackUrl { get; set; }
        public string PingUrl { get; set; }
    }

    
}
