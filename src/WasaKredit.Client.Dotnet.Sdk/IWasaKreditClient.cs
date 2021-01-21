using System;
using System.Threading;
using System.Threading.Tasks;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Requests;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Responses;

namespace WasaKredit.Client.Dotnet.Sdk
{
    public interface IWasaKreditClient{

        CalculateMonthlyCostResponse CalculateMonthlyCost(CalculateMonthlyCostRequest request);

        Task<CalculateMonthlyCostResponse> CalculateMonthlyCostAsync(CalculateMonthlyCostRequest request, CancellationToken cancellationToken = default(CancellationToken));


        ValidateFinancedAmountResponse ValidateFinancedAmount(string amount);

        Task<ValidateFinancedAmountResponse> ValidateFinancedAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken));

        Task<ValidateInvoiceFinancedAmountResponse> ValidateInvoiceFinancedAmountAsync(string amount,
            CancellationToken cancellationToken = default(CancellationToken));

        ValidateInvoiceFinancedAmountResponse ValidateInvoiceFinancedAmount(string amount);
        GetMonthlyCostWidgetResponse GetMonthlyCostWidget(string amount);

        Task<GetMonthlyCostWidgetResponse> GetMonthlyCostWidgetAsync(string amount, CancellationToken cancellationToken = default(CancellationToken));

        CreateCheckoutResponse CreateLeasingCheckout(CreateCheckoutRequest request);
        Task<CreateCheckoutResponse> CreateLeasingCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default(CancellationToken));
        CreateInvoiceCheckoutResponse CreateInvoiceCheckout(CreateInvoiceCheckoutRequest request);
        Task<CreateInvoiceCheckoutResponse> CreateInvoiceCheckoutAsync(CreateInvoiceCheckoutRequest request, CancellationToken cancellationToken = default(CancellationToken));

        [Obsolete] //use CreateLeasingCheckout
        CreateCheckoutResponse CreateCheckout(CreateCheckoutRequest request);
        
        [Obsolete] //use CreateLeasingCheckout
        Task<CreateCheckoutResponse> CreateCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default(CancellationToken));

        GetOrderResponse GetOrder(string orderId);

        Task<GetOrderResponse> GetOrderAsync(string orderId, CancellationToken cancellationToken = default(CancellationToken));

        GetOrderStatusResponse GetOrderStatus(string orderId);

        Task<GetOrderStatusResponse> GetOrderStatusAsync(string orderId, CancellationToken cancellationToken = default(CancellationToken));

        ShipOrderResponse ShipOrder(ShipOrderRequest request);
        Task<ShipOrderResponse> ShipOrderAsync(ShipOrderRequest request,CancellationToken cancellationToken = default(CancellationToken));
        CancelOrderResponse CancelOrder(CancelOrderRequest request);
        Task<CancelOrderResponse> CancelOrderAsync(CancelOrderRequest request, CancellationToken cancellationToken = default(CancellationToken));

        AddOrderReferenceResponse AddOrderReference(string orderId, AddOrderReferenceRequest request);

        Task<AddOrderReferenceResponse> AddOrderReferenceAsync(string orderId, AddOrderReferenceRequest request, CancellationToken cancellationToken = default(CancellationToken));
        GetPaymentOptionsResponse GetLeasingPaymentOptions(string totalAmount);

        Task<GetPaymentOptionsResponse> GetLeasingPaymentOptionsAsync(string totalAmount,
            CancellationToken cancellationToken = default(CancellationToken));
    }
}
