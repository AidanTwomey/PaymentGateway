using System;
using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
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