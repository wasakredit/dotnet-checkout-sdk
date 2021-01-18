namespace WasaKredit.Client.Dotnet.Sdk.Contracts
{
    public class InvoiceCartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public CurrencyAmount PriceExVat { get; set; }
        public CurrencyAmount PriceInclVat { get; set; }
        public int Quantity { get; set; }
        public string VatPercentage { get; set; }
        public CurrencyAmount VatAmount { get; set; }
        public CurrencyAmount TotalPriceInclVat { get; set; }
        public CurrencyAmount TotalPriceExVat { get; set; }
        public CurrencyAmount TotalVat { get; set; }

    }
}
