﻿using System.Collections.Generic;

namespace WasaKredit.Client.Dotnet.Sdk.Contracts.GetPaymentMethods
{
    public class GetPaymentMethodsResponse
    {
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
    }
}