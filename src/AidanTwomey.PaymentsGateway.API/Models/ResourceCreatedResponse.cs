using System;

namespace AidanTwomey.Paymentsgateway.API.Controllers
{
    public class PaymentCreatedResponse
    {
        public Guid Id { get; set;}
        public string CardNumber { get; set; }
    }
}