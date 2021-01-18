using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WasaKredit.Client.Dotnet.Sdk.Authentication;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Requests;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Responses;
using WasaKredit.Client.Dotnet.Sdk.Exceptions;
using WasaKredit.Client.Dotnet.Sdk.RestClient;
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
        /// Calculates the monthly cost for each product based on the default contract length which is preconfigured for you as a Wasa Kredit partner.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CalculateMonthlyCostResponse CalculateMonthlyCost(CalculateMonthlyCostRequest request)
        {
            
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/leasing/monthly-cost");

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
            
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/leasing/monthly-cost");

            var response = await _restClient
                .PostAsync<CalculateMonthlyCostRequest, CalculateMonthlyCostResponse>(url, System.Net.HttpStatusCode.OK, request, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }


        /// <summary>
        /// Calculates the cost for all available payment methods and contract lengths (if applicable) for you as a partner to Wasa Kredit.
        /// </summary>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        public GetPaymentMethodsResponse GetPaymentMethods(string totalAmount)
        {
            Authorize();
            var url = $"{CheckoutGateWayApiUrl}/v4/payment-methods?total_amount={totalAmount}";
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
            await AuthorizeAsync();

            var url = $"{CheckoutGateWayApiUrl}/v4/payment-methods?total_amount={totalAmount}";
            var response = await _restClient.GetAsync<GetPaymentMethodsResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer", cancellationToken);
            return response.Result;
        }

     

        /// <summary>
        /// Validates whether a leasing amount is within the min/max financing amount limits for you as a Wasa Kredit partner using  Leasing as payment method.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public ValidateFinancedAmountResponse ValidateFinancedAmount(string amount)
        {
            
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/leasing/validate-financed-amount/?amount=", amount);

            var response = _restClient.Get<ValidateFinancedAmountResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer");

            return response.Result;
        }

        /// <summary>
        /// Asynchronously validates whether a leasing amount is within the min/max financing amount limits for you as a Wasa Kredit partner using  Leasing as payment method.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ValidateFinancedAmountResponse> ValidateFinancedAmountAsync(string amount, CancellationToken cancellationToken = default(CancellationToken))
        {
            
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/leasing/validate-financed-amount/?amount=", amount);

            var response = await _restClient.GetAsync<ValidateFinancedAmountResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }


        /// <summary>
        /// Validates whether an invoice amount is within the min/max financing amount limits for you as a Wasa Kredit partner using Invoice as payment method.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public ValidateInvoiceFinancedAmountResponse ValidateInvoiceFinancedAmount(string amount)
        {

            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/invoice/validate-financed-amount/?amount=", amount);

            var response = _restClient.Get<ValidateInvoiceFinancedAmountResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer");

            return response.Result;
        }

        /// <summary>
        /// Asynchronously validates whether an invoice amount is within the min/max financing amount limits for you as a Wasa Kredit partner using Invoice as payment method.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<ValidateInvoiceFinancedAmountResponse> ValidateInvoiceFinancedAmountAsync(string amount)
        {

            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/invoice/validate-financed-amount/?amount=", amount);

            var response = await _restClient.GetAsync<ValidateInvoiceFinancedAmountResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer");

            return response.Result;
        }



        public GetMonthlyCostWidgetResponse GetMonthlyCostWidget(string amount)
        {
            
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/leasing/widgets/monthly-cost?amount={amount}&currency=SEK");

            var response = _restClient.Get<string>(url, HttpStatusCode.OK, _authorizationToken.Token);

            return new GetMonthlyCostWidgetResponse
            {
                HtmlSnippet = response.ResultString
            };
        }

        public async Task<GetMonthlyCostWidgetResponse> GetMonthlyCostWidgetAsync(string amount, CancellationToken cancellationToken = default(CancellationToken))
        {
            
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/leasing/widgets/monthly-cost?amount={amount}&currency=SEK");

            var response = await _restClient.GetAsync<string>(url, HttpStatusCode.OK, _authorizationToken.Token, cancellationToken: cancellationToken);

            return new GetMonthlyCostWidgetResponse
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
            
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/leasing/checkouts");

            var response = _restClient.Post<CreateCheckoutRequest, string>(url, System.Net.HttpStatusCode.Created, request, _authorizationToken.Token);
            
            return new CreateCheckoutResponse { HtmlSnippet = response.ResultString };
        }

        public CreateCheckoutResponse CreateLeasingCheckout(CreateCheckoutRequest request)
        {
            return CreateCheckout(request);
        }

        /// <summary>
        /// Asynchronously creates a checkout instance and provides the checkout as a html snippet that is supposed to be embedded in your checkout view.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CreateCheckoutResponse> CreateCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/leasing/checkouts");

            var response = await _restClient.PostAsync<CreateCheckoutRequest, string>(url, System.Net.HttpStatusCode.Created, request, _authorizationToken.Token, "Bearer", cancellationToken);

            return new CreateCheckoutResponse { HtmlSnippet = response.ResultString };
        }

        public async Task<CreateCheckoutResponse> CreateLeasingCheckoutAsync(CreateCheckoutRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await CreateCheckoutAsync(request, cancellationToken);
        }


        /// <summary>
        /// Creates a invoice-checkout instance and provides the checkout as a html snippet that is supposed to be embedded in your invoice-checkout view.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CreateInvoiceCheckoutResponse CreateInvoiceCheckout(CreateInvoiceCheckoutRequest request)
        {

            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/invoice/checkouts");

            var response = _restClient.Post<CreateInvoiceCheckoutRequest, string>(url, System.Net.HttpStatusCode.Created, request, _authorizationToken.Token);

            return new CreateInvoiceCheckoutResponse { HtmlSnippet = response.ResultString };
        }

        /// <summary>
        /// Creates a invoice-checkout instance and provides the checkout as a html snippet that is supposed to be embedded in your invoice-checkout view.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CreateInvoiceCheckoutResponse> CreateInvoiceCheckoutAsync(CreateInvoiceCheckoutRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {

            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/invoice/checkouts");

            var response = await _restClient.PostAsync<CreateInvoiceCheckoutRequest, string>(url, System.Net.HttpStatusCode.Created, request, _authorizationToken.Token,"Bearer", cancellationToken);

            return new CreateInvoiceCheckoutResponse { HtmlSnippet = response.ResultString };
        }


        /// <summary>
        /// Fetches a Wasa Kredit order based on its id.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public GetOrderResponse GetOrder(string orderId)
        {
            
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/orders/", orderId);
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
            
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/orders/", orderId);
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
            
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/orders/{orderId}/status");
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
            
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, "/v4/orders/{orderId}/status", orderId);
            var response =await _restClient.GetAsync<GetOrderStatusResponse>(url, System.Net.HttpStatusCode.OK, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Sets the Wasa Kredit order status to shipped. This method should be used to set the Wasa Kredit order status to shipped, if you have shipped the order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ShipOrderResponse ShipOrder(ShipOrderRequest request)
        {
            
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/orders/{request.OrderId}/ship");

            var response = _restClient.Post<ShipOrderRequest, ShipOrderResponse>(url, System.Net.HttpStatusCode.OK, null, _authorizationToken.Token);

            return response.Result;
        }


        /// <summary>
        /// Asynchronously sets the Wasa Kredit order status to shipped. This method should be used to set the Wasa Kredit order status to shipped, if you have shipped the order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShipOrderResponse> ShipOrderAsync(ShipOrderRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {

            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/orders/{request.OrderId}/ship");

            var response = await _restClient.PostAsync<ShipOrderRequest, ShipOrderResponse>(url, System.Net.HttpStatusCode.OK, null, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Sets the Wasa Kredit order status to canceled. This method should be used to set the Wasa Kredit order status to canceled, if you have canceled the order.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CancelOrderResponse CancelOrder(CancelOrderRequest request)
        {

            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/orders/{request.OrderId}/cancel");

            var response = _restClient.Post<CancelOrderRequest, CancelOrderResponse>(url, System.Net.HttpStatusCode.OK, null, _authorizationToken.Token);

            return response.Result;
        }


        /// <summary>
        /// Asynchronously sets the Wasa Kredit order status to canceled. This method should be used to set the Wasa Kredit order status to canceled, if you have canceled the order.

        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CancelOrderResponse> CancelOrderAsync(CancelOrderRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {

            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/orders/{request.OrderId}/cancel");

            var response = await _restClient.PostAsync<CancelOrderRequest, CancelOrderResponse>(url, System.Net.HttpStatusCode.OK, null, _authorizationToken.Token, "Bearer", cancellationToken);

            return response.Result;
        }

        /// <summary>
        /// Adds a new order reference and appends it to the current order references of the order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public AddOrderReferenceResponse AddOrderReference(string orderId, AddOrderReferenceRequest request)
        {
            
            Authorize();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/orders/{orderId}/order-references");

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
            
            await AuthorizeAsync();

            var url = string.Concat(CheckoutGateWayApiUrl, $"/v4/orders/{orderId}/order-references");

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
