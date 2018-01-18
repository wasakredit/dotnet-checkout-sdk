namespace WasaKredit.Client.Dotnet.Sdk.Models
{
    public class LeasingCost
    {
        public Price MonthlyCost { get; set; }

        public string ProductId { get; set; }

        public bool Leasable { get; set; }
    }
}
