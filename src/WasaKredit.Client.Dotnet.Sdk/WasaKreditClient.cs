using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WasaKredit.Client.Dotnet.Sdk.Authentication;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Requests;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Responses;
using WasaKredit.Client.Dotnet.Sdk.RestClient;
using WasaKredit.Client.Dotnet.Sdk.Exceptions;
using static WasaKredit.Client.Dotnet.Sdk.Settings;

[assembly: InternalsVisibleTo("WasaKredit.Client.Dotnet.Sdk.Tests")]

namespace WasaKredit.Client.Dotnet.Sdk
{
    /// <summary>
    /// A singleton class representing the Wasa Checkout API client. The singleton instance is accessed through the "Instance" property.
    /// </summary>
    public sealed class WasaKreditClient : IWasaKreditClient
    {
        private static WasaKreditClient _instance;
        private static IRestClient _restClient;
        private IAuthenticationClient _authenticationClient;
        private bool _testMode = false;
        private AccessToken _authorizationToken;
        private static readonly string _currency = "SEK";

        /// <summary>
        /// Accesses the singleton instance of the Wasa Checkout API client.
        /// </summary>
        public static WasaKreditClient Instance
        {
            get { return _instance ?? (_instance = new WasaKreditClient()); }
        }

        private string CheckoutGateWayApiUrl => _testMode ? TEST_CHECKOUT_GATEWAY_API_URL : CHECKOUT_GATEWAY_API_URL;

        public WasaKreditClient(){}

        /// <summary>
        /// Initializes the singleton instance of the Wasa Checkout API client.
        /// </summary>
        /// <param name="authenticationClient">The AuthenticationClient used to authenticate against WasaKredit Checkout Gateway API</param>
        /// <param name="testMode">Indicated whether to execute the operations in test mode or not.</param>
        /// <param name="requestTimeout">The request timeout in seconds.</param>
        public void Initialize(IAuthenticationClient authenticationClient, bool testMode, int requestTimeout = 30)
        {
            _restClient = new RestClient.RestClient(TimeSpan.FromSeconds(requestTimeout), testMode);
            _authenticationClient = authenticationClient;
            
            _testMode = testMode;
            _authenticationClient.SetTestMode(testMode);
        }

        /// <summary>
        /// Calculates the monthly leasing cost for each product based on the default contract length which is preconfigured for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("CalculateLeasingCost is deprecated, use CalculateMonthlyCost instead")]
        public CalculateLeasingCostResponse CalculateLeasingCost(CalculateLeasingCostRequest request)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/leasing");

            var response = _restClient
                            .Post<CalculateLeasingCostRequest, CalculateLeasingCostResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token);
            
            return response.Result;
        }

        /// <summary>
        /// Asynchronously calculates the monthly leasing cost for each product based on the default contract length which is preconfigured for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Obsolete("CalculateLeasingCostAsync is deprecated, use CalculateMonthlyCostAsync instead")]
        public async Task<CalculateLeasingCostResponse> CalculateLeasingCostAsync(CalculateLeasingCostRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/leasing");

            var response = await _restClient.PostAsync<CalculateLeasingCostRequest, CalculateLeasingCostResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Calculates the monthly cost for each product based on the default contract length which is preconfigured for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CalculateMonthlyCostResponse CalculateMonthlyCost(CalculateMonthlyCostRequest request)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/monthly-cost");

            var response = _restClient
                .Post<CalculateMonthlyCostRequest, CalculateMonthlyCostResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token);

            return response.Result;
        }

        /// <summary>
        /// Asynchronously calculates the monthly cost for each product based on the default contract length which is preconfigured for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CalculateMonthlyCostResponse> CalculateMonthlyCostAsync(CalculateMonthlyCostRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/monthly-cost");

            var response = await _restClient
                .PostAsync<CalculateMonthlyCostRequest, CalculateMonthlyCostResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Calculates the total monthly leasing costs for a total amount. The monthly leasing cost will be provided for each of your available contract lengths as a partner to Wasa Kredit.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("CalculateTotalLeasingCost is deprecated, do not use.")]
        public CalculateTotalLeasingCostResponse CalculateTotalLeasingCost(CalculateTotalLeasingCostRequest request)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/leasing/total-cost");

            var response = _restClient
                .Post<CalculateTotalLeasingCostRequest, CalculateTotalLeasingCostResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token);

            return response.Result;
        }

        /// <summary>
        /// Asynchronously calculates the total monthly leasing costs for a total amount. The monthly leasing cost will be provided for each of your available contract lengths as a partner to Wasa Kredit.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Obsolete("CalculateTotalLeasingCostAsync is deprecated, do not use.")]
        public async Task<CalculateTotalLeasingCostResponse> CalculateTotalLeasingCostAsync(CalculateTotalLeasingCostRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/leasing/total-cost");

            var response = await _restClient.PostAsync<CalculateTotalLeasingCostRequest, CalculateTotalLeasingCostResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Calculates the cost for all available payment methods and contract lengths (if applicable) for you as a partner to Wasa Kredit.
        /// </summary>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        public GetPaymentMethodsResponse GetPaymentMethods(string totalAmount)
        {
            CheckInitialized();
            Authorize();

            var url = $"{CheckoutGateWayApiUrl}/v1/payment-methods?total_amount={totalAmount}&currency={_currency}";
            var response = _restClient.Get<GetPaymentMethodsResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token);
            return response.Result;
        }

        /// <summary>
        /// Asynchronously calculates the cost for all available payment methods and contract lengths (if applicable) for you as a partner to Wasa Kredit.
        /// </summary>
        /// <param name="totalAmount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetPaymentMethodsResponse> GetPaymentMethodsAsync(string totalAmount, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            Authorize();

            var url = $"{CheckoutGateWayApiUrl}/v1/payment-methods?total_amount={totalAmount}&currency={_currency}";
            var response = await _restClient.GetAsync<GetPaymentMethodsResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer", cancellationToken);
            return response.Result;
        }

        /// <summary>
        /// Validates whether an amount is within the min/max financing amount limits for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        [Obsolete("ValidateLeasingAmount is deprecated, use ValidateFinancedAmount instead.")]
        public ValidateLeasingAmountResponse ValidateLeasingAmount(string amount)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/leasing/validate-amount?amount=", amount);

            var response = _restClient.Get<ValidateLeasingAmountResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer");

            return response.Result;
        }

        /// <summary>
        /// Asynchronously validates whether an amount is within the min/max financing amount limits for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Obsolete("ValidateLeasingAmountAsync is deprecated, use ValidateFinancedAmountAsync instead.")]
        public async Task<ValidateLeasingAmountResponse> ValidateLeasingAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/leasing/validate-amount?amount=", amount);

            var response = await _restClient.GetAsync<ValidateLeasingAmountResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Validates whether an amount is within the min/max financing amount limits for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public ValidateFinancedAmountResponse ValidateFinancedAmount(string amount)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/validate-financed-amount/?amount=", amount);

            var response = _restClient.Get<ValidateFinancedAmountResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer");

            return response.Result;
        }

        /// <summary>
        /// Asynchronously validates whether an amount is within the min/max financing amount limits for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Obsolete("ValidateLeasingAmountAsync is deprecated, use ValidateFinancedAmountAsync instead.")]
        public async Task<ValidateFinancedAmountResponse> ValidateFinancedAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/validate-financed-amount/?amount=", amount);

            var response = await _restClient.GetAsync<ValidateFinancedAmountResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        public GetMonthlyCostWidgetResponse GetMonthlyCostWidget(string amount)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v2/widgets/monthly-cost?amount={amount}&currency=SEK");

            var response = _restClient.Get<string>(url, HttpStatusCode.OK, _authorizationToken.Token);

            return new GetMonthlyCostWidgetResponse
            {
                HtmlSnippet = response.ResultString
            };
        }

        public async Task<GetMonthlyCostWidgetResponse> GetMonthlyCostWidgetAsync(string amount, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v2/widgets/monthly-cost?amount={amount}&currency=SEK");

            var response = await _restClient.GetAsync<string>(url, HttpStatusCode.OK, _authorizationToken.Token, cancellationToken: cancellationToken);

            return new GetMonthlyCostWidgetResponse
            {
                HtmlSnippet = response.ResultString
            };
        }

        /// <summary>
        /// Creates and provides a Product Widget, in the form of a html snippet, that may be displayed close to the price information on the product details view on your e-commerce site.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Obsolete("CreateProductWidget is obsolete, use GetMonthlyCostWidget instead")]
        public CreateProductWidgetResponse CreateProductWidget(CreateProductWidgetRequest request)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/checkouts/widget");

            var response = _restClient.Post<CreateProductWidgetRequest, string>(url, System.Net.HttpStatusCode.Created, request, _authorizationToken.Token);

            return new CreateProductWidgetResponse
            {
                HtmlSnippet = response.ResultString
            };
        }

        /// <summary>
        /// Asynchronously creates and provides a Product Widget, in the form of a html snippet, that may be displayed close to the price information on the product details view on your e-commerce site.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [Obsolete("CreateProductWidgetAsync is obsolete, use GetMonthlyCostWidgetAsync instead")]
        public async Task<CreateProductWidgetResponse> CreateProductWidgetAsync(CreateProductWidgetRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/checkouts/widget");

            var response = await _restClient.PostAsync<CreateProductWidgetRequest, string>(url, System.Net.HttpStatusCode.Created, request, _authorizationToken.Token, "Bearer", cancellationToken);

            return new CreateProductWidgetResponse
            {
                HtmlSnippet = response.ResultString
            };
        }

        /// <summary>
        /// Creates a checkout instance and provides the checkout as a html snippet that is supposed to be embedded in your checkout view.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreateCheckoutResponse CreateCheckout(CreateCheckoutRequest request)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v2/checkouts");

            var response = _restClient.Post<CreateCheckoutRequest, string>(url, System.Net.HttpStatusCode.Created, request, _authorizationToken.Token);
            
            return new CreateCheckoutResponse { HtmlSnippet = response.ResultString };
        }

        /// <summary>
        /// Asynchronously creates a checkout instance and provides the checkout as a html snippet that is supposed to be embedded in your checkout view.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CreateCheckoutResponse> CreateCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v2/checkouts");

            var response = await _restClient.PostAsync<CreateCheckoutRequest, string>(url, System.Net.HttpStatusCode.Created, request, _authorizationToken.Token, "Bearer", cancellationToken);

            return new CreateCheckoutResponse { HtmlSnippet = response.ResultString };
        }

        /// <summary>
        /// Fetches a Wasa Kredit order based on its id.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public GetOrderResponse GetOrder(string orderId)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/orders/", orderId);
            var response = _restClient.Get<GetOrderResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token);

            return response.Result;
        }

        /// <summary>
        /// Asynchronously fetches a Wasa Kredit order based on its id.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetOrderResponse> GetOrderAsync(string orderId, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/orders/", orderId);
            var response = await _restClient.GetAsync<GetOrderResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Gets the current status of a Wasa Kredit order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public GetOrderStatusResponse GetOrderStatus(string orderId)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v1/orders/{orderId}/status");
            var response = _restClient.Get<GetOrderStatusResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token);

            return response.Result;
        }

        /// <summary>
        /// Asynchronously gets the current status of a Wasa Kredit order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<GetOrderStatusResponse> GetOrderStatusAsync(string orderId, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v1/orders/{orderId}/status", orderId);
            var response =await _restClient.GetAsync<GetOrderStatusResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Changes the status of the Wasa Kredit order. This method should be used to update the Wasa Kredit order status if you have shipped or canceled the order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public UpdateOrderStatusResponse UpdateOrderStatus(UpdateOrderStatusRequest request)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v1/orders/{request.OrderId}/status/{request.Status.Status}");

            var response = _restClient.Put<UpdateOrderStatusRequest, UpdateOrderStatusResponse>(url, System.Net.HttpStatusCode.OK, null, _authorizationToken.Token);

            return response.Result;
        }

        /// <summary>
        /// Asynchronously changes the status of the Wasa Kredit order. This method should be used to update the Wasa Kredit order status if you have shipped or canceled the order.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UpdateOrderStatusResponse> UpdateOrderStatusAsync(UpdateOrderStatusRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v1/orders/{request.OrderId}/status/{request.Status.Status}");

            var response = await _restClient.PutAsync<CreateCheckoutRequest, string>(url, System.Net.HttpStatusCode.OK, null, _authorizationToken.Token, "Bearer", cancellationToken);

            return new UpdateOrderStatusResponse { Status = response.Result };
        }

        /// <summary>
        /// Adds a new order reference and appends it to the current order references of the order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public AddOrderReferenceResponse AddOrderReference(string orderId, AddOrderReferenceRequest request)
        {
            CheckInitialized();
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v1/orders/{orderId}/order-references");

            var response = _restClient.Post<AddOrderReferenceRequest, AddOrderReferenceResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token);

            return response.Result;
        }

        /// <summary>
        /// Asynchronously adds a new order reference and appends it to the current order references of the order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<AddOrderReferenceResponse> AddOrderReferenceAsync(string orderId, AddOrderReferenceRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckInitialized();
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v1/orders/{orderId}/order-references");

            var response = await _restClient.PostAsync<AddOrderReferenceRequest, AddOrderReferenceResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }
        
        private async Task AuthorizeAsync()
        {
            CheckInitialized();
            _authorizationToken = await _authenticationClient.GetAccessTokenAsync();
        }

        private void Authorize()
        {
            CheckInitialized();
            _authorizationToken = _authenticationClient.GetAccessToken();
        }

        private void CheckInitialized()
        {
            if (_restClient == null || _authenticationClient == null)
            {
                throw new WasaKreditClientException("The Wasa Checkout API client has not been initialized.");
            }
        }

        public static string GetExceptionMessage(WasaKreditApiException ex)
        {
            StringBuilder errorStringBuilder = new StringBuilder();
            errorStringBuilder.AppendLine("A WasaKreditApiException occured.");
            errorStringBuilder.Append(string.IsNullOrEmpty(ex.Error.ErrorCode) ? string.Empty : $"Error code: {ex.Error.ErrorCode}" + Environment.NewLine);
            errorStringBuilder.Append(string.IsNullOrEmpty(ex.Error.Description) ? string.Empty : ex.Error.Description + Environment.NewLine);
            errorStringBuilder.Append(string.IsNullOrEmpty(ex.Error.DeveloperMessage) ? string.Empty : ex.Error.DeveloperMessage + Environment.NewLine);
            errorStringBuilder.Append(string.IsNullOrEmpty(ex.Error.FriendlyResourceName) ? string.Empty : ex.Error.FriendlyResourceName + Environment.NewLine);
            errorStringBuilder.Append(string.IsNullOrEmpty(ex.Error.ResourceId) ? string.Empty : ex.Error.ResourceId + Environment.NewLine);

            if (ex.Error.InvalidProperties != null)
            {
                foreach (var invalidProperty in ex.Error.InvalidProperties)
                {
                    errorStringBuilder.AppendLine($"{invalidProperty.Property}, {invalidProperty.ErrorMessage}");
                }
            }

            return errorStringBuilder.ToString();
        }

        public static string GetExceptionMessage(WasaKreditAuthenticationException ex)
        {
            StringBuilder errorStringBuilder = new StringBuilder();
            errorStringBuilder.AppendLine("A WasaKreditAuthenticationException occured.");
            errorStringBuilder.Append($"Status code: {ex.StatusCode}" + Environment.NewLine);
            errorStringBuilder.Append(string.IsNullOrEmpty(ex.Message) ? string.Empty : $"Message: {ex.Message}" + Environment.NewLine);

            return errorStringBuilder.ToString();
        }
    }
}
