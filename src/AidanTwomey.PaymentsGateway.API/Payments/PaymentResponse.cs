using System;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public struct PaymentResponse
    {
        public PaymentResponse(Guid id)
        {
            Id = id;
        }

        public readonly Guid Id;
    }
}