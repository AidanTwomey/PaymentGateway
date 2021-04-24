namespace AidanTwomey.PaymentGateway.API.Model
{
    public class MakePaymentRequest
    {
        public Card Card { get; set;}
        public decimal Amount { get; set;}
        public string Currency { get; set;}
    }
}