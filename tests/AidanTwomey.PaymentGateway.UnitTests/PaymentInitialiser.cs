using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Validation;

namespace AidanTwomey.PaymentGateway.UnitTests
{
    public static class PaymentInitialiser
    {
        public static (MakePaymentRequest, PaymentValidator) InitialiseRequest(string cardNumber, int month, int year, decimal amount) =>
            (new MakePaymentRequest() { Card = new(cardNumber, month, year), Amount = amount }, 
            new PaymentValidator());
    }
}
