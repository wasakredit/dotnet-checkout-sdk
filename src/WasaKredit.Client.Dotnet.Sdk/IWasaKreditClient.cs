using System;
using System.Threading;
using System.Threading.Tasks;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Requests;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Responses;

namespace WasaKredit.Client.Dotnet.Sdk
{
    public interface IWasaKreditClient
    {
        [Obsolete("CalculateLeasingCost is deprecated, use CalculateMonthlyCost instead.")]
        CalculateLeasingCostResponse CalculateLeasingCost(CalculateLeasingCostRequest request);

        [Obsolete("CalculateLeasingCostAsync is deprecated, use CalculateMonthlyCostAsync instead.")]
        Task<CalculateLeasingCostResponse> CalculateLeasingCostAsync(CalculateLeasingCostRequest request, CancellationToken cancellationToken = default(CancellationToken));

        CalculateMonthlyCostResponse CalculateMonthlyCost(CalculateMonthlyCostRequest request);

        Task<CalculateMonthlyCostResponse> CalculateMonthlyCostAsync(CalculateMonthlyCostRequest request, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("CalculateTotalLeasingCost is deprecated, do not use.")]
        CalculateTotalLeasingCostResponse CalculateTotalLeasingCost(CalculateTotalLeasingCostRequest request);

        [Obsolete("CalculateTotalLeasingCostAsync is deprecated, do not use.")]
        Task<CalculateTotalLeasingCostResponse> CalculateTotalLeasingCostAsync(CalculateTotalLeasingCostRequest request, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("ValidateLeasingAmount is deprecated, use ValidateFinancedAmount instead.")]
        ValidateLeasingAmountResponse ValidateLeasingAmount(string amount);

        [Obsolete("ValidateLeasingAmountAsync is deprecated, use ValidateFinancedAmountAsync instead.")]
        Task<ValidateLeasingAmountResponse> ValidateLeasingAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken));

        ValidateFinancedAmountResponse ValidateFinancedAmount(string amount);

        Task<ValidateFinancedAmountResponse> ValidateFinancedAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken));

        GetMonthlyCostWidgetResponse GetMonthlyCostWidget(string amount);

        Task<GetMonthlyCostWidgetResponse> GetMonthlyCostWidgetAsync(string amount, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete("CreateProductWidget is obsolete, use GetMonthlyCostWidget instead")]
        CreateProductWidgetResponse CreateProductWidget(CreateProductWidgetRequest request);

        [Obsolete("CreateProductWidgetAsync is obsolete, use GetMonthlyCostWidgetAsync instead")]
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
