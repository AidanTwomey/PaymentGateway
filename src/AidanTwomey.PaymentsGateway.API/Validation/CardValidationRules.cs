using System;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
    internal static class CardValidationRules
    {
        public static bool NumberIsCorrectLength(MakePaymentRequest request) => request.Card.Number.Length == 16;
        public static bool NumberHasOnlyDigits(MakePaymentRequest request) => Int64.TryParse(request.Card.Number, out _);        
    }

    internal static class PaymentValidationRules
    {
        public static bool AmountIsGreaterThanZero(MakePaymentRequest request) => request.Amount > 0;       
    }
}