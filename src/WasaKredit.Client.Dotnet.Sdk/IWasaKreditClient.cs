using System.Threading;
using System.Threading.Tasks;
using WasaKredit.Client.Dotnet.Sdk.Requests;
using WasaKredit.Client.Dotnet.Sdk.Responses;

namespace WasaKredit.Client.Dotnet.Sdk
{
    public interface IWasaKreditClient
    {
        [System.Obsolete("CalculateLeasingCost is deprecated, use CalculateMonthlyCost instead.")]
        CalculateLeasingCostResponse CalculateLeasingCost(CalculateLeasingCostRequest request);

        [System.Obsolete("CalculateLeasingCostAsync is deprecated, use CalculateMonthlyCostAsync instead.")]
        Task<CalculateLeasingCostResponse> CalculateLeasingCostAsync(CalculateLeasingCostRequest request, CancellationToken cancellationToken = default(CancellationToken));

        CalculateMonthlyCostResponse CalculateMonthlyCost(CalculateMonthlyCostRequest request);

        Task<CalculateMonthlyCostResponse> CalculateMonthlyCostAsync(CalculateMonthlyCostRequest request, CancellationToken cancellationToken = default(CancellationToken));

        [System.Obsolete("CalculateTotalLeasingCost is deprecated, do not use.")]
        CalculateTotalLeasingCostResponse CalculateTotalLeasingCost(CalculateTotalLeasingCostRequest request);

        [System.Obsolete("CalculateTotalLeasingCostAsync is deprecated, do not use.")]
        Task<CalculateTotalLeasingCostResponse> CalculateTotalLeasingCostAsync(CalculateTotalLeasingCostRequest request, CancellationToken cancellationToken = default(CancellationToken));

        [System.Obsolete("ValidateLeasingAmount is deprecated, use ValidateFinancedAmount instead.")]
        ValidateLeasingAmountResponse ValidateLeasingAmount(string amount);

        [System.Obsolete("ValidateLeasingAmountAsync is deprecated, use ValidateFinancedAmountAsync instead.")]
        Task<ValidateLeasingAmountResponse> ValidateLeasingAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken));

        ValidateFinancedAmountResponse ValidateFinancedAmount(string amount);

        Task<ValidateFinancedAmountResponse> ValidateFinancedAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken));

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
