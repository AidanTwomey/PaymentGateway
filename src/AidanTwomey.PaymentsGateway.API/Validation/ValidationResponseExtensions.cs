using AidanTwomey.PaymentsGateway.API.Payments;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
    internal static class ValidationResponseExtensions
    {
        internal static Payment ToPayment(this ValidationResponse validatedRequest)
        {
            return new Payment(){Amount = validatedRequest.Transaction.Amount};
        }
    }
}