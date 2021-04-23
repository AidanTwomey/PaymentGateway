using AidanTwomey.PaymentsGateway.API.Payments;

namespace AidanTwomey.PaymentsGateway.API.Validation
{

    internal static class ValidationResponseExtensions
    {
        internal static Payment ToPayment(this ValidationResponse response)
        {
            return new Payment(){Card = response.Transaction.Card};
        }
    }
}