using System;
using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Contracts;
using WasaKredit.Client.Dotnet.Sdk.Contracts.GetPaymentMethods;
using WasaKredit.Client.Dotnet.Sdk.Requests;

namespace SamplesHelperClasses
{
    public static class RequestMockFactory
    {
        public static CalculateMonthlyCostRequest CalculateMonthlyCostRequest()
        {
            return new CalculateMonthlyCostRequest
            {
                Items = new List<Item>
                {
                    new Item {ProductId = "12345", FinancedPrice = new Price {Amount = "11500.50", Currency = "SEK"}},
                    new Item {ProductId = "23456", FinancedPrice = new Price {Amount = "17995.50", Currency = "SEK"}}
                }
            };
        }

        public static GetPaymentMethodsRequest GetPaymentMethodsRequest()
        {
            return new GetPaymentMethodsRequest {TotalAmount = new CurrencyAmount {Amount = "20000", Currency = "SEK"}};
        }

        public static CalculateTotalLeasingCostRequest CalculateTotalLeasingCostRequest()
        {
            return new CalculateTotalLeasingCostRequest
            {
                TotalAmount = new Price { Amount = "11500.50", Currency = "SEK" }
            };
        }

        public static CreateProductWidgetRequest CreateProductWidgetRequest()
        {
            return new CreateProductWidgetRequest
            {
                FinancialProduct = "Leasing",
                PriceExVat = new Price
                {
                    Amount = "1299.50",
                    Currency = "SEK"
                }
            };
        }

        public static CreateCheckoutRequest CreateCheckoutRequest()
        {
            return new CreateCheckoutRequest
            {
                PaymentTypes = "leasing",
                OrderReferenceId = "a1be9394-182d-49c7-a470-ea59e68ce3ef",
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
                            VatAmount = new Price {Amount = "3748.75", Currency = "SEK"},
                            Quantity = 2,
                            VatPercentage = "25",
                            ImageUrl = "https://unsplash.it/500/500"
                        }
                    },
                ShippingCostExVat = new Price { Amount = "995", Currency = "SEK" },
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
                RecipientPhone = "070-1234567"
            };
        }

        public static UpdateOrderStatusRequest UpdateOrderStatusRequest()
        {
            return new UpdateOrderStatusRequest
            {
                OrderId = "1257d019-9ba7-4a25-90a4-21788854bc56",
                Status = new OrderStatus { Status = "shipped" }
            };
        }

        public static AddOrderReferenceRequest AddOrderReferenceRequest()
        {
            return new AddOrderReferenceRequest { Key = "Quote", Value = Guid.NewGuid().ToString() };
        }
    }
}