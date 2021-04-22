using System;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Command
{
    public record PaymentTransaction
    {
        private readonly Card card;

        public PaymentTransaction(Card card, decimal amount, DateTime timestamp, bool success)
        {
            this.card = card;
            Amount = amount;
            Timestamp = timestamp;
            Success = success;
        }

        public Card Method { get; init;}
        public decimal Amount { get; init;}
        public DateTime Timestamp { get; init;}
        public bool Success { get; init;}
        
    }
}