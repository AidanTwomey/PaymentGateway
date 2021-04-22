using System;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public class SuccessfulPaymentResponse : PaymentResponse
    {
        public SuccessfulPaymentResponse(Guid id) : base(id)
        {
        }
    }

    public class FailedPaymentResponse : PaymentResponse
    {
        public FailedPaymentResponse(Guid id) : base(id)
        {
        }
    }

    public abstract class PaymentResponse
    {
        public PaymentResponse(Guid id)
        {
            Id = id;
        }

        public readonly Guid Id;
    }
}