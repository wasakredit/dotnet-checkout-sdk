using System;
using System.Collections.Generic;
using System.Linq;
using SamplesHelperClasses;
using WasaKredit.Client.Dotnet.Sdk;
using WasaKredit.Client.Dotnet.Sdk.Authentication;
using WasaKredit.Client.Dotnet.Sdk.Exceptions;

namespace DotnetCore31
{
    class Program
    {
        private static AuthenticationClient _authenticationClient;

        static void Main(string[] args)
        {
            _authenticationClient = AuthenticationClient.Instance;
            //When using testmode, use test_client_id and _test_client_secret which should be supplied by WasaKredit. 
            _authenticationClient.SetClientCredentials("#Client_Id#", "#Client_Secret#");

            CalculateMonthlyCostExample();
            GetPaymentMethodsExample();
            ValidateFinancedAmountExample();
            CreateMonthlyCostWidgetExample();
            CreateCheckoutExample();
            GetOrderExample();
            GetOrderStatusExample();
//            UpdateOrderStatusExample();
//TODO Replace with ship- and cancel order
            AddOrderReferenceExample();

            MultiplePartnersExample();
        }

        private static void CalculateMonthlyCostExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                var response = client.CalculateMonthlyCost(RequestMockFactory.CalculateMonthlyCostRequest());
                var firstItemMonthlyCost = response.MonthlyCosts.ToList()[0];

                Console.WriteLine($"The monthly cost for product {firstItemMonthlyCost.ProductId}: {firstItemMonthlyCost.MonthlyCost.Amount} SEK.");
                Console.ReadLine();
            }
            catch (WasaKreditApiException ex)
            {
                PrintException(ex);
            }
            catch (WasaKreditAuthenticationException ex)
            {
                PrintException(ex);
            }
        }

        private static void GetPaymentMethodsExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                var response = client.GetPaymentMethods("20000");
                var firstPaymentMethod = response.PaymentMethods.ToList()[0];

                Console.WriteLine($"Payment method is \"{firstPaymentMethod.DisplayName}\" with a default contract length of {firstPaymentMethod.Options.DefaultContractLength} months.");
                Console.ReadLine();
            }
            catch (WasaKreditApiException ex)
            {
                PrintException(ex);
            }
            catch (WasaKreditAuthenticationException ex)
            {
                PrintException(ex);
            }
        }

        public static void ValidateFinancedAmountExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                string amount = "10000.00";
                var response = client.ValidateFinancedAmount(amount);

                Console.WriteLine($"Validation result for the amount {amount} SEK is {response.ValidationResult}.");
                Console.ReadLine();
            }
            catch (WasaKreditApiException ex)
            {
                PrintException(ex);
            }
            catch (WasaKreditAuthenticationException ex)
            {
                PrintException(ex);
            }
        }

        public static void CreateMonthlyCostWidgetExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                var response = client.GetMonthlyCostWidget("10000");
                Console.WriteLine($"Product widget html snippet:\n{response.HtmlSnippet}");
                Console.ReadLine();
            }
            catch (WasaKreditApiException ex)
            {
                PrintException(ex);
            }
            catch (WasaKreditAuthenticationException ex)
            {
                PrintException(ex);
            }
        }

        public static void CreateCheckoutExample(WasaKreditClient client = null)
        {
            if (client == null)
            {
                client = WasaKreditClient.Instance;
                client.Initialize(_authenticationClient, true);
            }

            try
            {
                var response = client.CreateCheckout(RequestMockFactory.CreateCheckoutRequest());
                Console.WriteLine($"Checkout html snippet:\n{response.HtmlSnippet}");
                Console.ReadLine();
            }
            catch (WasaKreditApiException ex)
            {
                PrintException(ex);
            }
            catch (WasaKreditAuthenticationException ex)
            {
                PrintException(ex);
            }
        }

        public static void GetOrderExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                var response = client.GetOrder(Guid.NewGuid().ToString()); //Should be replaced for an actual order id.
                Console.WriteLine(
                    $"Order with order reference {response.OrderReferences.FirstOrDefault().Key}: {response.OrderReferences.FirstOrDefault().Value} and customer organization number {response.CustomerOrganizationNumber}.");
                Console.ReadLine();
            }
            catch (WasaKreditApiException ex)
            {
                PrintException(ex);
            }
            catch (WasaKreditAuthenticationException ex)
            {
                PrintException(ex);
            }
        }

        public static void GetOrderStatusExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                string orderId = Guid.NewGuid().ToString(); //Should be replaced for an actual order id.
                var response = client.GetOrderStatus(orderId);

                Console.WriteLine($"The status of order {orderId} is \"{response.Status}\".");
                Console.ReadLine();
            }
            catch (WasaKreditApiException ex)
            {
                PrintException(ex);
            }
            catch (WasaKreditAuthenticationException ex)
            {
                PrintException(ex);
            }
        }

        //public static void UpdateOrderStatusExample()
        //{
        //    var client = WasaKreditClient.Instance;
        //    client.Initialize(_authenticationClient, true);

        //    try
        //    {
        //        var request = RequestMockFactory.UpdateOrderStatusRequest();
        //        var response = client.UpdateOrderStatus(request);
        //        Console.WriteLine($"The status for order {request.OrderId} has been set to {response.Status}.");
        //        Console.ReadLine();
        //    }
        //    catch (WasaKreditApiException ex)
        //    {
        //        PrintException(ex);
        //    }
        //    catch (WasaKreditAuthenticationException ex)
        //    {
        //        PrintException(ex);
        //    }
        //}

        private static void AddOrderReferenceExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                string orderId = Guid.NewGuid().ToString(); //Should be replaced for an actual order id.
                var response = client.AddOrderReference(orderId, RequestMockFactory.AddOrderReferenceRequest());
                Console.WriteLine($"The order with id {orderId} now holds the following {response.OrderReferences.Count()} references:");

                foreach (var orderReference in response.OrderReferences)
                {
                    Console.WriteLine($"\t{orderReference.Key} - {orderReference.Value}");
                }

                Console.ReadLine();
            }
            catch (WasaKreditApiException ex)
            {
                PrintException(ex);
            }
            catch (WasaKreditAuthenticationException ex)
            {
                PrintException(ex);
            }
        }

        private static void MultiplePartnersExample()
        {
            var wasaKreditClients = SetupMultiplePartners();

            CreateCheckoutExample(wasaKreditClients["PartnerOneId"]);
            CreateCheckoutExample(wasaKreditClients["PartnerTwoId"]);

            //reuse access tokens 
            CreateCheckoutExample(wasaKreditClients["PartnerOneId"]);
            CreateCheckoutExample(wasaKreditClients["PartnerTwoId"]);
        }

        private static Dictionary<string, WasaKreditClient> SetupMultiplePartners()
        {
            var credentialsPartnerOne = ("PartnerOneId", "PartnerOneSecret");
            var credentialsPartnerTwo = ("PartnerTwoId", "PartnerTwoSecret");

            // Setup AuthenticationClients and WasaKreditClients for all Partners
            var authenticationClientPartnerOne = new AuthenticationClient();
            authenticationClientPartnerOne.SetClientCredentials(
                credentialsPartnerOne.Item1,
                credentialsPartnerOne.Item2);

            var authenticationClientPartnerTwo = new AuthenticationClient();
            authenticationClientPartnerTwo.SetClientCredentials(
                credentialsPartnerTwo.Item1,
                credentialsPartnerTwo.Item2);

            var wasaClientPartnerOne = new WasaKreditClient();
            wasaClientPartnerOne.Initialize(authenticationClientPartnerOne, true);

            var wasaClientPartnerTwo = new WasaKreditClient();
            wasaClientPartnerTwo.Initialize(authenticationClientPartnerTwo, true);

            // Preferably, the WasaKreditClients should be setup on application startup and 
            // kept available for reuse during runtime via your dependency injector.
            var wasaKreditClients = new Dictionary<string, WasaKreditClient>();
            wasaKreditClients.Add(credentialsPartnerOne.Item1, wasaClientPartnerOne);
            wasaKreditClients.Add(credentialsPartnerTwo.Item1, wasaClientPartnerTwo);

            return wasaKreditClients;
        }

        private static void PrintException(WasaKreditApiException ex)
        {
            Console.Write(WasaKreditClient.GetExceptionMessage(ex));
        }

        private static void PrintException(WasaKreditAuthenticationException ex)
        {
            Console.Write(WasaKreditClient.GetExceptionMessage(ex));
        }
    }
}
