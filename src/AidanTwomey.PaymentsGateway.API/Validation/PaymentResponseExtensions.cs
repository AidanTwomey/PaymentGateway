using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.Paymentsgateway.API.Controllers;
using AidanTwomey.PaymentsGateway.API.Payments;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
    public static class PaymentResponseExtensions
    {
        public static PaymentCreatedResponse ToResponse(this PaymentResponse response)
        {
            var paymentCreated = new PaymentCreatedResponse()
            {
                Id = response.Id
            };

            if (response is SuccessfulPaymentResponse successfulPayment)
                paymentCreated.CardNumber = successfulPayment.Card.ToString();

            return paymentCreated;
        }
    }
}