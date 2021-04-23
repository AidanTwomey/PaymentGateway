using System;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public class SuccessfulPaymentResponse : PaymentResponse
    {
        public SuccessfulPaymentResponse(Guid id, Card card) : base(id)
        {
            this.Card = card;
        }

        public Card Card { get; private set;}

        public override bool Rejected => false;
    }

    public class FailedPaymentResponse : PaymentResponse
    {
        public FailedPaymentResponse(Guid id) : base(id)
        {
        }

        public override bool Rejected => true;
    }

    public abstract class PaymentResponse
    {
        public PaymentResponse(Guid id)
        {
            Id = id;
        }

        public readonly Guid Id;

        public abstract bool Rejected { get;}
    }
}