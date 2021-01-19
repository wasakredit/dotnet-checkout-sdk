using System;
using System.Collections.Generic;
using System.Text;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Requests
{
    public class CreateInvoiceCheckoutRequest
    {
        public IEnumerable<OrderReference> OrderReferences { get; set; }
        public IEnumerable<InvoiceCartItem> CartItems { get; set; }
        public Price TotalPriceInclVat { get; set; }
        public Price TotalPriceExVat { get; set; }
        public Price TotalVat { get; set; }
        public string CustomerOrganizationNumber { get; set; }
        public string PurchaserName { get; set; }
        public string PurchaserEmail { get; set; }
        public string PurchaserPhone { get; set; }
        public string PartnerReference { get; set; }
        public BillingDetails BillingDetails { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string RequestDomain { get; set; }
        public string ConfirmationCallbackUrl { get; set; }
        public string PingUrl { get; set; }
    }

}
