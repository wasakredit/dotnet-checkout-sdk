namespace WasaKredit.Client.Dotnet.Sdk.Models
{
    public class CartItem
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public Price PriceExVat { get; set; }

        public int Quantity { get; set; }

        public string VatPercentage { get; set; }

        public Price VatAmount { get; set; }

        public string ImageUrl { get; set; }
    }
}
