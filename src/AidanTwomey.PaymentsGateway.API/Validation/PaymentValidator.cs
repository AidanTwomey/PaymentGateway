using System;
using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
    internal static class CardValidationRules
    {
        public static bool NumberIsCorrectLength(MakePaymentRequest request) => request.Card.Number.Length == 16;
        public static bool NumberHasOnlyDigits(MakePaymentRequest request) => Int64.TryParse(request.Card.Number, out _);        
    }
    public class PaymentValidator : IPaymentValidator
    {
        public ValidationResponse Validate(MakePaymentRequest request) =>
            Validate(request, DateTime.Now);

        public ValidationResponse Validate(MakePaymentRequest request, DateTime asOf)
        {
            return RunRules(
                CardValidationRules.NumberIsCorrectLength,
                CardValidationRules.NumberHasOnlyDigits,
                ExpiryIsInFuture
            );

            bool ExpiryIsInFuture(MakePaymentRequest request) => 
                asOf.Month <= request.Card.ExpiryMonth  && asOf.Year <= request.Card.ExpiryYear;

            ValidationResponse RunRules(params Func<MakePaymentRequest, bool>[] rules)
            {
                foreach (var rule in rules)
                {
                    if (!rule(request))
                        return new InvalidPayment();
                }

                return new ValidPayment();
            }
        }
    }
}