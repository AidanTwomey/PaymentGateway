namespace AidanTwomey.PaymentGateway.API.Model
{
    public record Card
    {
        public Card(string number, int expiryMonth, int expiryYear)
        {
            this.Number = number;
            this.ExpiryMonth = expiryMonth;
            this.ExpiryYear = expiryYear;
        }

        public string Number { get; init;}
        public int ExpiryMonth { get; init;}
        public int ExpiryYear { get; init;}
        public string Cvv { get;init;}

        public override string ToString()
        {
            return  $"************{Number.Substring(12,4)}";
        }
    }
}