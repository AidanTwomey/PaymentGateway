using System;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.Domain;

namespace AidanTwomey.PaymentsGateway.API.Validation
{
    public class ValidPayment : ValidationResponse
    {
        private readonly PaymentTransaction transaction;
        public ValidPayment(MakePaymentRequest paymentRequest)
        {
            this.transaction = new PaymentTransaction(
                                    paymentRequest.Card,
                                    paymentRequest.Amount,
                                    DateTime.Now,
                                    true
                                    );
        }

        public override PaymentTransaction Transaction { get => this.transaction; }
    }

    public class InvalidPayment : ValidationResponse
    {
        public override PaymentTransaction Transaction { get => throw new NotImplementedException(); }
    }

    public abstract class ValidationResponse
    {
        public abstract PaymentTransaction Transaction { get; }
    }
}