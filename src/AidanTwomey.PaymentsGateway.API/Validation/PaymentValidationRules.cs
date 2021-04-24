using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
    internal static class PaymentValidationRules
    {
        public static bool AmountIsGreaterThanZero(MakePaymentRequest request) => request.Amount > 0;       
    }
}