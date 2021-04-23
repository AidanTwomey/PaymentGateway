namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public class ProcessedPayment
    {
        public string id { get;set;}
        public string cardNumber { get; set;}
        public decimal amount { get; set;}
        public bool success { get; set;}
    }
}