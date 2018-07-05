# WasaKredit Checkout API .NET Client SDK

## Changelog

* 2018-07-05: Added support for multiple partners in the same application instance
* 2017-12-13: Corrected response types for get and update order

**Table of Content**

* [Getting started](#getting_started)
* [Test Projects](#test_projects)
* [Initialization](#initialization)
* [Available Methods](#available_methods)
  * [CalculateMonthlyCost](#calculate_monthly_cost)
  * [GetPaymentMethods](#get_payment_methods)
  * [ValidateFinancedAmount](#validate_financed_amount)
  * [CreateProductWidget](#create_product_widget)
  * [CreateCheckout](#create_checkout)
  * [Handling custom checkout callbacks](#custom_callbacks)
  * [GetOrder](#get_order)
  * [GetOrderStatus](#get_order_status)
  * [UpdateOrderStatus](#update_order_status)
  * [AddOrderReference](#add_order_reference)
* [Exception Handling](#exception_handling)

## <a name="getting_started">Getting Started</a>

### Prerequisites

* Microsoft .NET Framework 4.5 or above, .NET Core 1.0 or above.
* Partner credentials provided by WasaKredit.

### Acquiring the SDK

The .NET SDK is available at NuGet and GitHub as described below. To be able to use the SDK you must apply for Partner credentials by sending an e-mail to ehandel@wasakredit.se.

#### NuGet

The .NET SDK is available as a nuget package with package id "WasaKredit.Checkout.SDK"

#### GitHub

The .NET SDK is available at GitHub at https://github.com/wasakredit/dotnet-checkout-sdk. The GitHub SDK version also includes two sample console projects illustrating the usage of the SDK.

## <a name="test_projects">Test Projects</a>

There are two sample console projects included in the SDK. Notice that these sample projects are only available in the GitHub version of the SDK.
* **DotnetCore20** - Exemplifies how to initialize and how to use the SDK.
* **DotnetFramework45**  - Shows the necessary dependency setup. The usage of the SDK is identical to the examples in the DotnetCore20 project.

## <a name="initialization">Initialization</a>

Before utilizing any of the SDK methods the authentication and the Wasa Kredit API client must be initialized.

If your platform only handles one Partner, you can use the Singleton implementation of the AuthenticationClient and WasaKreditClient.

If your platform will handle multiple Partners in the same application scope, you need to create instances of AuthenticationClient and WasaKreditClient for each Partner.

### Initialization single partner

#### Authentication

Authentication is done in two steps.
1. Access the authentication client singleton instance through the AuthenticationClient.Instance property.
2. Set your client credentials (client id and client secret), provided by Wasa Kredit, by calling the SetClientCredentials method. Thereafter you will be automatically authenticated, provided that the client credentials were valid.

```c#
var authenticationClient = AuthenticationClient.Instance;
authenticationClient.SetClientCredentials("<your client id here>", "<your client secret here>");
```

#### WasaKreditClient

The WasaKreditClient is the client used to access the SDK methods. The client is accessed and initialized in two steps.
1. Access the Wasa Kredit API client singleton instance through the WasaKreditClient.Instance property.
2. Initialize the client by calling the Initialize method, passing the authentication client and a boolean flag that indicated whether or not to use test mode as parameters.

```c#
bool testMode = true;
var wasaKreditClient = WasaKreditClient.Instance;
wasaKreditClient.Initialize(authenticationClient, testMode);
```

### Initialization multiple partners

Instead of using the singleton instances as described for single partners above, you need to create separate AuthenticationClients and WasaKreditClients for each Partner in your application.

#### Authentication

Authentication is done in two steps for each partner.
1. Create a new AuthenticationClient.
2. Set the client credentials (client id and client secret), provided by Wasa Kredit, by calling the SetClientCredentials method. The client will request and cache access tokens automatically when used.

```c#
var authenticationClient1 = new AuthenticationClient();
authenticationClient1.SetClientCredentials("<partner1 client id>", "<partner1 client secret>");

var authenticationClient2 = new AuthenticationClient();
authenticationClient2.SetClientCredentials("<partner2 client id>", "<partner2 client secret>");

```

#### WasaKreditClient

The WasaKreditClient is the client used to access the SDK methods. The client is initialized in two steps.
1. Create a new instance of the WasaKreditClient for each Partner in your application.
2. Initialize the client by calling the Initialize method, passing the authentication client of that Partner and a boolean flag indication whether or not to use testmode for the requests.

```c#
bool testModePartner1 = true;
var wasaKreditClient1 = new WasaKreditClient();
wasaKreditClient1.Initialize(authenticationClient1, testModePartner1);

bool testModePartner2 = true;
var wasaKreditClient2 = new WasaKreditClient();
wasaKreditClient2.Initialize(authenticationClient2, testModePartner2);
```

In the example above, _wasaKreditClient1_ should be used when making API requests for Partner 1 while Partner 2 has its own instance in _wasaKreditClient2_.

In order to utilize cached access tokens and easier use of the SDK, you should aim to register your WasaKreditClients at startup of your application and keep them available for reuse by resolving the correct client for each Partner during runtime. A good way to do this is via dependency injection.


## <a name="available_methods">Available Methods</a>

### <a name="calculate_monthly_cost">CalculateMonthlyCost</a>

This method calculates the monthly cost for each product based on the default contract length which is preconfigured for you as a Wasa Kredit partner. The most obvious usage for this method is to provide monthly prices for each product in product list views on your e-commerce site.

```c#
CalculateMonthlyCostResponse CalculateMonthlyCost(CalculateMonthlyCostRequest request)
```

#### Parameters

##### CalculateMonthlyCostRequest

| Name | Type | Description |
|---|---|---|
| Items | *List[**Item**]* (required) | A list containing product **Item**s |

##### Item

| Name | Type | Description |
|---|---|---|
| FinancedPrice | *Price* (required) | ... |
| ProductId | *string* (required) | Your unique product identifier |

##### Price

| Name | Type | Description |
|---|---|---|
| Amount | *string* (required) | The amount/price represented as a numeric string. The maximum length is 10 characters, including the decimal delimiter. Prices are supposed to be specified using up to 7 integer digits and two decimal digits. Specifying the decimal part is optional. The period character (‘.’) is used as the decimal delimiter.
| Currency | *string* (required) | The currency represented as a ISO 4217 currency code. *At present, only SEK is handled*.|

#### Response

##### CalculateMonthlyCostResponse

| Name | Type | Description |
|---|---|---|
| MonthlyCosts | *List[**MonthlyCost**]* | A list containing the monthly cost for each requested **Item** |
| ProductId | *string* | Your unique product identifier |

##### MonthlyCost

| Name | Type | Description |
|---|---|---|
| MonthlyCost | Price | ... |

##### Price

| Name | Type | Description |
|---|---|---|
| Amount | *string* | The amount/price represented as a numeric string. The maximum length is 10 characters, including the decimal delimiter. Prices are supposed to be specified using up to 7 integer digits and two decimal digits. Specifying the decimal part is optional. The period character (‘.’) is used as the decimal delimiter.
| Currency | *string* | The currency represented as a ISO 4217 currency code. *At present, only SEK is handled*. |

#### Example usage

```c#
var request = new CalculateMonthlyCostRequest
{
    Items = new List<Item>
    {
        new Item {ProductId = "12345", FinancedPrice = new Price {Amount = "11500.50", Currency = "SEK"}},
        new Item {ProductId = "23456", FinancedPrice = new Price {Amount = "17995.50", Currency = "SEK"}}
    }
};

var response = await wasaKreditClient.CalculateMonthlyCostAsync(request);

```

### <a name="get_payment_methods">GetPaymentMethods</a>

This method calculates the cost for all available payment methods and contract lengths (if applicable) for you as a partner to Wasa Kredit. This method is especially useful if you are offering multiple Wasa Kredit payment methods. 

```c#
GetPaymentMethodsResponse GetPaymentMethods(string totalAmount)
```

#### Parameters

| Name | Type | Description |
|---|---|---|
| totalAmount | *string* (required) | The total financed amount. |

#### Response

##### GetPaymentMethodsResponse

| Name | Type | Description |
|---|---|---|
| PaymentMethods | *List[**PaymentMethod**]* | A list containing the information for each of your available Wasa Kredit payment methods. |

##### PaymentMethod

| Name | Type | Description |
|---|---|---|
| Id | string | The payment method identifier name. *Ex: "leasing" or "rental"* |
| DisplayName | string | The payment method as a friendly name. *Ex: "Leasing" or "Rental"* |
| Options | **Options** | An object containing various options such as contract lenghts, costs etc. for each individual payment method. |

##### Options
**Please notice that the options object is dynamic and will differ for different payment methods. For leasing and rental it is structured in the following way.**

| Name | Type | Description |
|---|---|---|
| DefaultContractLength | int | The default contract length which will be pre-selected in the checkout. |
| ContractLengths | *List[**ContractLengthItem**]* | Your available contract lengths and their monthly costs. |

##### ContractLengthItem

| Name | Type | Description |
|---|---|---|
| ContractLength | int | The contract length in months. |
| MonthlyCost | **CurrencyAmount** | The monthly cost and currency for the contract length. |

##### CurrencyAmount

| Name | Type | Description |
|---|---|---|
| Amount | string | The amount. |
| Currency | string | The currency. |

#### Example usage

```c#
string amount = "20000";
var response = await wasaKreditClient.GetPaymentMethods(amount);
```

### <a name="validate_financed_amount">ValidateFinancedAmount</a>

Validates that an amount is within the min/max financing amount for you as a Wasa Kredit partner. The primary purpose of this method is to validate whether the Wasa Kredit monthly payment option should be displayed for a given cart amount or not.

```c#
ValidateFinancedAmountResponse ValidateFinancedAmount(string amount)
```

#### Parameters

| Name | Type | Description |
|---|---|---|
| Amount | *string* (required) | The total financed amount. |

#### Response

##### ValidateFinancedAmountResponse

| Name | Type | Description |
|---|---|---|
| ValidationResult | *bool* | The validation result. |

#### Example usage

```c#
var response = client.ValidateFinancedAmount("10000.00");
```

### <a name="create_product_widget">CreateProductWidget</a>

To inform the customer about Wasa Kredit financing as an available payment method, this method provides a Product Widget, in the form of a html snippet, that may be displayed close to the price information on the product details view on your e-commerce site.

```c#
CreateProductWidgetResponse CreateProductWidget(CreateProductWidgetRequest request)
```

#### Parameters

##### CreateProductWidgetRequest

| Name | Type | Description |
|---|---|---|
| FinancialProduct | *string* (required) | The financial product. *Ex: "Leasing"*. |
| PriceExVat | *Price* (required) | The product price excluding VAT. |

#### Response

##### CreateProductWidgetResponse

| Name | Type | Description |
|---|---|---|
| HtmlSnippet | *string* | The product widget snippet for embedding. |

#### Example usage

```c#
var request = new CreateProductWidgetRequest
{
    FinancialProduct = "Leasing",
    PriceExVat = new Price
    {
        Amount = "1299.50",
        Currency = "SEK"
    }
};

var response = client.CreateProductWidget(request);
```

### <a name="create_checkout">CreateCheckout</a>

The Checkout is the most central object in the Wasa Kredit B2B Checkout product. The checkout is supposed to be inserted as a payment method in your e-commerce checkout. This method creates a checkout instance and provides the checkout as a html snippet that is supposed to be embedded in your checkout view.

```c#
CreateCheckoutResponse CreateCheckout(CreateCheckoutRequest request)
```

#### Parameters

##### CreateCheckoutRequest

| Name | Type | Description |
|---|---|---|
| PaymentTypes | *string* | Selected payment type to use in the checkout, e.g. 'leasing'. |
| OrderReferences | *List[**OrderReference**]* | A list containing order reference objects. |
| CartItem | *List[Cart Item]* (required) | A list of the items in the cart as Cart Item objects. |
| ShippingCostExVat | *Price* (required) | Price object containing the shipping cost excluding VAT. |
| CustomerOrganizationNumber | *string* | Customer organization number. |
| PurchaserName | *string* | Name of the purchaser. |
| PurchaserEmail | *string* | E-mail of the purchaser. |
| PurchaserPhone | *string* | Phone number of the purchaser. |
| BillingAddress | *Address* | Address object containing the billing address. |
| DeliveryAddress | *Address* | Address object containing the delivery address. |
| RecipientName | *string* | Name of the recipient. |
| RecipientPhone | *string* | Phone number of the recipient. |
| RequestDomain | *string* | The domain of the partner, used to allow CORS. |
| ConfirmationCallbackUrl | *string* | Url to the partner's confirmation page. |
| PingUrl | *string* | Receiver url for order status change pingback notifications. |

##### OrderReference

| Name | Type | Description |
|---|---|---|
| Key | *string* (required) | A key to describe the reference. *Ex: "temp_order_reference"*. |
| Value | *string* (required) | The actual order reference value. |

##### Cart Item

| Name | Type | Description |
|---|---|---|
| ProductId | *string* (required) | The product identifier. |
| ProductName | *string* (required) | Name of the product. |
| PriceExVat | *Price* (required) | Price object containing the price of the product excluding VAT. |
| Quantity | *int* (required) | Quantity of the product. |
| VatPercentage | *string* (required) | VAT percentage as a parsable string, e.g. '25' is 25%.  |
| VatAmount | *Price* (required) | Price object containing the calculated VAT of the product. |
| ImageUrl | *string* | Product image URL. |

##### Price

| Name | Type | Description |
|---|---|---|
| Amount | *string* (required) | A string value that will be parsed to a decimal, e.g. 199 is '199.00'. |
| Currency | *string* (required) | The currency represented as a ISO 4217 currency code. *At present, only SEK is handled*. |

##### Address

| Name | Type | Description |
|---|---|---|
| CompanyName | *string* | Company name |
| StreetAddress | *string* | Street address |
| PostalCode | *string* | Postal code |
| City | *string* | City |
| Country | *string* | Country |

#### Response

##### CreateCheckoutResponse

| Name | Type | Description |
|---|---|---|
| HtmlSnippet | *string* | The checkout snippet for embedding. |

#### Example usage

```c#
var request = new CreateCheckoutRequest
{
    PaymentTypes = "leasing",
    OrderReferences = new List<OrderReference>
    {
        new OrderReference {Key = "QuoteReference", Value = "a40261e0-7c25-496a-a2ac-c7b93adb81bf"}
    },
    CartItems =
        new List<CartItem>
        {
            new CartItem
            {
                ProductId = "ez",
                ProductName = "Kylskåp EZ3",
                PriceExVat = new Price {Amount = "14995.10", Currency = "SEK"},
                VatAmount = new Price {Amount = "3748.78", Currency = "SEK"},
                Quantity = 2,
                VatPercentage = "25",
                ImageUrl = "https://unsplash.it/500/500"
            }
        },
    ShippingCostExVat = new Price {Amount = "995", Currency = "SEK"},
    CustomerOrganizationNumber = "7904174757",
    PurchaserName = "Anders Svensson",
    PurchaserEmail = "purchaser@gmail.com",
    PurchaserPhone = "070-1234567",
    BillingAddress =
        new Address
        {
            CompanyName = "Star Republic",
            StreetAddress = "Ekelundsgatan 9",
            PostalCode = "41118",
            City = "Göteborg",
            Country = "Sweden"
        },
    DeliveryAddress =
        new Address
        {
            CompanyName = "Star Republic",
            StreetAddress = "Ekelundsgatan 9",
            PostalCode = "41118",
            City = "Göteborg",
            Country = "Sweden"
        },
    RecipientName = "Anders Svensson",
    RecipientPhone = "070-1234567",
    RequestDomain = "https://www.wasa-partner.se",
    ConfirmationCallbackUrl = "https://www.wasa-partner.se/payment-callback/",
    PingUrl = "https://www.wasa-partner.se/payment-callback/"
};

var response = client.CreateCheckout(request);
```

Note that the `OrderReferences` property is a collection. Even if you don't want to create an order in your system at before creating a Wasa Kredit checkout, you have the possibility to supply a temporary identifier to be able to match the Wasa Kredit order with some reference in your system. You also have the option to add additional reference identifiers at a later time, for example when your final order is created

The URL that you supply through the `PingUrl` property should be an endpoint that is set up to receive a POST message and return an http status code 200 response on success.

You will receive an html snippet which you should embed in your web page, inside of which the Wasa Kredit Checkout widget will handle the payment flow.

### <a name="initialize_checkout">Initialize checkout</a>

After creating a Wasa Kredit Checkout by calling the `CreateCheckout` method and embedding the resulting html snippet in your web page, as described above, the checkout html snippet needs to be explicitly initialized through a javascript call to the global `window.wasaCheckout.init()` function. The `init` method call will populate the \<div\> contained in the html snippet and link it to an internal iframe.

```javascript
<script>
    window.wasaCheckout.init();
</script>
```

### <a name="custom_callbacks">Handling custom checkout callbacks</a>

Optionally, you're able to pass an options object to the `init`-function. Use this if you want to manually handle the onComplete, onRedirect and onCancel events.

```javascript
<script>
    var options = {
      onComplete: function(orderReferences){
        //[...]
      },
      onRedirect: function(orderReferences){
        //[...]
      },
      onCancel: function(orderReferences){
        //[...]
      }
    };   
    window.wasaCheckout.init(options);
</script>
```

The `onComplete` event will be raised when a user has completed the checkout process. We recommend that you convert your cart/checkout to an order here if you haven't done it already.

The `onRedirect` event will be raised the user clicks the "back to store/proceed"-button.

The `onCancel` event will be raised if the checkout process is canceled by the user or Wasa Kredit.

All callback functions will get the `orderReferences` parameter passed from the checkout. This parameter consists of an Array of `KeyValue` objects.

These are the same values as the ones that was passed to the `CreateCheckout`-method through the `OrdeReferences` property and also Wasa Kredit order id.

```javascript
orderReferences = [
    { key: "wasakredit-order-id", value: "9c722707-123a-44e7-9eba-93e3a372d57e" },
    { key: "partner_checkout_id", value: "900123" },
    { key: "partner_reserved_order_number", value: "123456" }
];    
```

### <a name="get_order">GetOrder</a>

Fetches a Wasa Kredit order based on its id.

When a checkout is created (by calling the CreateCheckout method) a Wasa Kredit *order* is created. Anytime the status of the order is changed, a pingback notification will be sent to the PingUrl specified when the checkout was created. The body if this notification contains the ID of the order and the order status. The order id received via the notification may be used to fetch the entire order object by calling the `GetOrder` method.

```c#
GetOrderResponse GetOrder(string orderId)
```

#### Parameters

| Name | Type | Description |
|---|---|---|
| OrderId | *string* (required) | The order identifier. |

#### Response

##### GetOrderResponse

| Name | Type | Description |
|---|---|---|
| CustomerOrganizationNumber | *string* | The organization number of the customer who made the purchase. |
| BillingAddress | *Address* | ... |
| DeliveryAddress | *Address* | ... |
| OrderReferences | *List[**OrderReference**]* | A list containing order reference objects. |
| PurchaserEmail | *string* | The email of the person performing the purchase. |
| RecipientName | *string* | The name of the person who should receive the order. |
| RecipientPhone | *string* | The phone number of the person who should receive the order. |
| Status | *OrderStatus* | The status that the order is in at Wasa Kredit. |
| CartItem | *List[Cart Item]* | A list of the items purchased as Cart Item objects. |

##### Address

| Name | Type | Description |
|---|---|---|
| CompanyName | *string* | Company name |
| StreetAddress | *string* | Street address |
| PostalCode | *string* | Postal code |
| City | *string* | City |
| Country | *string* | Country |

##### OrderReference

| Name | Type | Description |
|---|---|---|
| Key | *string* | A key to succinctly describe the reference. *Ex: "temp_order_reference"*. |
| Value | *string* | The actual order reference value. |

##### OrderStatus

| Name | Type | Description |
|---|---|---|
| Status | *string* | The status that the order is in at Wasa Kredit. |

###### <a name="order_statuses">Possible order statuses</a>

* **initialized** - The order has been created but the order agreement has not yet been signed by the customer.
* **canceled** - The purchase was not approved by Wasa Kredit or it has been canceled by you as a partner. If you have created an order in your system it can safely be deleted.
* **pending** - The checkout has been completed and a customer has signed the order agreement, but additional signees is required or the order has not yet been fully approved by Wasa Kredit.
* **ready\_to\_ship** - All necessary signees have signed the order agreement and the order has been fully approved by Wasa Kredit. The order must now be shipped to the customer before Wasa Kredit will issue the payment to you as a partner.
* **shipped** - This status is set by the partner when the order item(s) have been shipped to the customer.

##### Cart Item

| Name | Type | Description |
|---|---|---|
| ProductId | *string* | The product identifier. |
| ProductName | *string* | Name of the product. |
| PriceExVat | *Price*  | Price object containing the price of the product excluding VAT. |
| Quantity | *int* | Quantity of the product. |
| VatPercentage | *string* | VAT percentage as a parsable string, e.g. '25' is 25%.  |
| VatAmount | *Price* | Price object containing the calculated VAT of the product. |
| ImageUrl | *string* | Product image URL. |

#### Example usage

```c#
string orderId = "891f4314-8ecc-4923-9f85-03dabf52df88";
var response = client.GetOrder(orderId);
```

### <a name="get_order_status">GetOrderStatus</a>

Gets the current status of a Wasa Kredit order.

When an order status change notification is received. This method may be called to check the current status of the order.

```c#
GetOrderStatusResponse GetOrderStatus(string orderId)
```

#### Parameters

| Name | Type | Description |
|---|---|---|
| OrderId | *string* (required) | The order identifier. |

#### Response

##### GetOrderStatusResponse

| Name | Type | Description |
|---|---|---|
| Status | *OrderStatus* | The order identifier. |

##### OrderStatus

| Name | Type | Description |
|---|---|---|
| Status | *string* | The current order status at Wasa Kredit. For a list of possible order statuses, see [Possible order statuses](#order_statuses) |

#### Example usage

```c#
string orderId = "891f4314-8ecc-4923-9f85-03dabf52df88";
var response = client.GetOrderStatus(orderId);
```

### <a name="update_order_status">UpdateOrderStatus</a>

Changes the status of the Wasa Kredit order. This method should be used to update the Wasa Kredit order status if you have shipped or canceled the order. Thus it is only possible to set the status to "canceled" or "shipped". The status can only be set to "canceled" if it has not already been shipped or completed and to "shipped" if its current status is "ready_to_ship."

```c#
UpdateOrderStatusResponse UpdateOrderStatus(UpdateOrderStatusRequest request)
```

#### Parameters

##### UpdateOrderStatusRequest

| Name | Type | Description |
|---|---|---|
| OrderId | *string* (required) | The order identifier. |
| Status | *OrderStatus* (required) | An order status object containing the new status. |

##### OrderStatus

| Name | Type | Description |
|---|---|---|
| Status | *string* | The order status. |

#### Response

##### UpdateOrderStatusResponse

| Name | Type | Description |
|---|---|---|
| Status | *OrderStatus* | An order status object containing the new status. |

##### OrderStatus

| Name | Type | Description |
|---|---|---|
| Status | *string* | The order status. |

#### Example usage

```c#
var request = new UpdateOrderStatusRequest
{
    OrderId = "1257d019-9ba7-4a25-90a4-21788854bc56",
    Status = new OrderStatus {Status = "shipped"}
};

var response = client.UpdateOrderStatus(request);
```

### <a name="add_order_reference">AddOrderReference</a>

Adds a new order reference and appends it to the collection of current order references of the order. The purpose of supporting multiple order references for a single order is to provide generic support for e-commerce platforms and solutions that use multiple references in their purchase and order flow.

```c#
AddOrderReferenceResponse AddOrderReference(string orderId, AddOrderReferenceRequest request)
```

#### Parameters

| Name | Type | Description |
|---|---|---|
| orderId | *string* (required) | The unique identifier for this order (Note: this is the id of the Wasa Kredit order). |
| request | *AddOrderReferenceRequest* (required) | The request object. |

##### AddOrderReferenceRequest

| Name | Type | Description |
|---|---|---|
| Key | *string* (required) | A key describing the order reference. |
| Value | *string* (required) | The order reference value. |

#### Response

##### AddOrderReferenceResponse

| Name | Type | Description |
|---|---|---|
| OrderReferences | *List[**OrderReference**]* | A list containing order reference objects. |

##### OrderReference

| Name | Type | Description |
|---|---|---|
| Key | *string* | A key describe the reference. *Ex: "temp_order_reference"*. |
| Value | *string* | The actual order reference value. |

#### Example usage

```c#
string orderId = "1257d019-9ba7-4a25-90a4-21788854bc56",
var request = new AddOrderReferenceRequest {Key = "Quote", Value = Guid.NewGuid().ToString()};

var response = client.AddOrderReference(orderId, request);
```

### <a name="exception_handling">Exception Handling</a>

The WasaKreditClient will throw the following exceptions

* **WasaKreditAuthenticationException** - Something went wrong during authentication. Probably, the client credentials were invalid.
* **WasaKreditApiException** - Something went wrong during the call from the SDK to the Wasa Kredit Checkout API. Probably the request were invalid (all required fields weren't specified or some field values were out of range). This exception will also occur due to an internal server error or other unforeseen error.

#### Exception Helper Methods

The WasaKreditClient contains the static *GetExceptionMessage* method for smooth exception message formatting. This method comes in two overloaded versions which takes a WasaKreditAuthenticationException respectively a WasaKreditApiException object as parameters.

```c#
static string GetExceptionMessage(WasaKreditApiException ex)
static string GetExceptionMessage(WasaKreditAuthenticationException ex)
```

example
```c#
try
{
    var response = client.CalculateMonthlyCost(request);
}
catch (WasaKreditApiException ex)
{
    Console.Write(WasaKreditClient.GetExceptionMessage(ex));
}
catch (WasaKreditAuthenticationException ex)
{
    Console.Write(WasaKreditClient.GetExceptionMessage(ex));
}
```
