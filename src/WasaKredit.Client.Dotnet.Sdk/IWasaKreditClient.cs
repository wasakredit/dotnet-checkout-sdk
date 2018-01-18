using System.Threading;
using System.Threading.Tasks;
using WasaKredit.Client.Dotnet.Sdk.Models.Responses;
using WasaKredit.Client.Dotnet.Sdk.Requests;
using WasaKredit.Client.Dotnet.Sdk.Response;

namespace WasaKredit.Client.Dotnet.Sdk
{
    public interface IWasaKreditClient
    {
        CalculateLeasingCostResponse CalculateLeasingCost(CalculateLeasingCostRequest request);

        Task<CalculateLeasingCostResponse> CalculateLeasingCostAsync(CalculateLeasingCostRequest request, CancellationToken cancellationToken = default(CancellationToken));

        CalculateTotalLeasingCostResponse CalculateTotalLeasingCost(CalculateTotalLeasingCostRequest request);

        Task<CalculateTotalLeasingCostResponse> CalculateTotalLeasingCostAsync(CalculateTotalLeasingCostRequest request, CancellationToken cancellationToken = default(CancellationToken));

        ValidateLeasingAmountResponse ValidateLeasingAmount(string amount);

        Task<ValidateLeasingAmountResponse> ValidateLeasingAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken));

        CreateProductWidgetResponse CreateProductWidget(CreateProductWidgetRequest request);

        Task<CreateProductWidgetResponse> CreateProductWidgetAsync(CreateProductWidgetRequest request, CancellationToken cancellationToken = default(CancellationToken));

        CreateCheckoutResponse CreateCheckout(CreateCheckoutRequest request);

        Task<CreateCheckoutResponse> CreateCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default(CancellationToken));

        GetOrderResponse GetOrder(string orderId);

        Task<GetOrderResponse> GetOrderAsync(string orderId, CancellationToken cancellationToken = default(CancellationToken));

        GetOrderStatusResponse GetOrderStatus(string orderId);

        Task<GetOrderStatusResponse> GetOrderStatusAsync(string orderId, CancellationToken cancellationToken = default(CancellationToken));

        UpdateOrderStatusResponse UpdateOrderStatus(UpdateOrderStatusRequest request);

        Task<UpdateOrderStatusResponse> UpdateOrderStatusAsync(UpdateOrderStatusRequest request, CancellationToken cancellationToken = default(CancellationToken));

        AddOrderReferenceResponse AddOrderReference(string orderId, AddOrderReferenceRequest request);

        Task<AddOrderReferenceResponse> AddOrderReferenceAsync(string orderId, AddOrderReferenceRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
