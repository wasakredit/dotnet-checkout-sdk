namespace WasaKredit.Client.Dotnet.Sdk.Contracts.Requests
{
    public class UpdateOrderStatusRequest
    {
        public string OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}