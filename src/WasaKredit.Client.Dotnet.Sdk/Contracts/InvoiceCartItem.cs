namespace WasaKredit.Client.Dotnet.Sdk.Contracts
{
    public class InvoiceCartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public Price PriceExVat { get; set; }
        public Price PriceInclVat { get; set; }
        public int Quantity { get; set; }
        public string VatPercentage { get; set; }
        public Price VatAmount { get; set; }
        public Price TotalPriceInclVat { get; set; }
        public Price TotalPriceExVat { get; set; }
        public Price TotalVat { get; set; }

    }
}
