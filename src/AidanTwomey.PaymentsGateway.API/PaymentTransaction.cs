using System;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.Domain
{
    public record NotFoundTransaction : PaymentTransaction
    {
        public NotFoundTransaction() : base(null, default(decimal), DateTime.Now, false)
        {
        }
    }
    public record PaymentTransaction
    {
        public PaymentTransaction(Card card, decimal amount, DateTime timestamp, bool success)
        {
            this.Card = card;
            Amount = amount;
            Timestamp = timestamp;
            Success = success;
        }

        public Card Card { get; init;}
        public decimal Amount { get; init;}
        public DateTime Timestamp { get; init;}
        public bool Success { get; init;}
        
    }
}