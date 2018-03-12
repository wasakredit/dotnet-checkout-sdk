using System;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SamplesHelperClasses;
using WasaKredit.Client.Dotnet.Sdk;
using WasaKredit.Client.Dotnet.Sdk.Authentication;
using WasaKredit.Client.Dotnet.Sdk.Models;

namespace DotnetCore20
{
    class Program
    {
        private static AuthenticationClient _authenticationClient;

        static void Main(string[] args)
        {
            _authenticationClient = AuthenticationClient.Instance;
            _authenticationClient.SetClientCredentials("#Client_Id#", "#Client_Secret#");

            CalculateMonthlyCostExample();
            ValidateFinancedAmountExample();
            CreateProductWidgetExample();
            CreateCheckoutExample();
            GetOrderExample();
            GetOrderStatusExample();
            UpdateOrderStatusExample();
            AddOrderReferenceExample();
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

        public static void CreateProductWidgetExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                var response = client.CreateProductWidget(RequestMockFactory.CreateProductWidgetRequest());
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

        public static void CreateCheckoutExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

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

        public static void UpdateOrderStatusExample()
        {
            var client = WasaKreditClient.Instance;
            client.Initialize(_authenticationClient, true);

            try
            {
                var request = RequestMockFactory.UpdateOrderStatusRequest();
                var response = client.UpdateOrderStatus(request);
                Console.WriteLine($"The status for order {request.OrderId} has been set to {response.Status}.");
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
