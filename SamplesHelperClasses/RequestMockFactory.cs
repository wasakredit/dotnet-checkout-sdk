﻿using System;
using System.Collections.Generic;
using WasaKredit.Client.Dotnet.Sdk.Contracts;
using WasaKredit.Client.Dotnet.Sdk.Contracts.Requests;

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

      

        public static CreateCheckoutRequest CreateCheckoutRequest()
        {
            return new CreateCheckoutRequest
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


        public static CreateInvoiceCheckoutRequest CreateInvoiceCheckout()
        {
            return new CreateInvoiceCheckoutRequest
            {
                OrderReferences = new List<OrderReference>
                {
                    new OrderReference {Key = "QuoteReference", Value = "a40261e0-7c25-496a-a2ac-c7b93adb81bf"}
                },
                CartItems =
                    new List<InvoiceCartItem>
                    {
                        new InvoiceCartItem
                        {
                            ProductId = "ez",
                            ProductName = "Kylskåp EZ3",
                            PriceExVat = new Price() {Amount = "14995.10", Currency = "SEK"},
                            VatAmount = new Price {Amount = "3748.75", Currency = "SEK"},
                            Quantity = 2,
                            VatPercentage = "25",
                            PriceInclVat = new Price{ Amount = "20000.85", Currency = "SEK"},
                            TotalPriceExVat = new Price{Amount = "300000",Currency = "SEK"},
                            TotalPriceInclVat = new Price{Amount = "300000",Currency = "SEK"},
                            TotalVat = new Price{Amount = "300000",Currency = "SEK"}
                        }
                    },
                CustomerOrganizationNumber = "7904174757",
                PurchaserName = "Anders Svensson",
                PurchaserEmail = "purchaser@gmail.com",
                PurchaserPhone = "070-1234567",
                TotalPriceExVat = new Price { Amount = "300000", Currency = "SEK" },
                TotalPriceInclVat = new Price { Amount = "300000", Currency = "SEK" },
                TotalVat = new Price { Amount = "300000", Currency = "SEK" },
                BillingDetails = new BillingDetails
                {
                    BillingReference = "ekonomiavdelningen",
                    BillingTag = "Ekonomi avdelningen"
                },
                ConfirmationCallbackUrl = "http://myeshop.com/landhere",
                PartnerReference = "Karl säljare",
                PingUrl = "http://myeshop.com/pinghere",

                RecipientName = "Anders Svensson",
                RecipientPhone = "070-1234567",
                RequestDomain = "http://myeshop"
            };
        }

        public static AddOrderReferenceRequest AddOrderReferenceRequest()
        {
            return new AddOrderReferenceRequest { Key = "Quote", Value = Guid.NewGuid().ToString() };
        }
    }
}