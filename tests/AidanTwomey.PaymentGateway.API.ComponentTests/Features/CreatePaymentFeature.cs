using Xbehave;
using AidanTwomey.PaymentGateway.API.ComponentTests.Fixtures;

namespace AidanTwomey.PaymentGateway.API.ComponentTests.Features
{
    public class CreatePaymentFeature
    {

        [Scenario]
        [CustomInlineAutoData("4716144209705113")]        
        public void CreatePaymentShouldSucceed(string card, CreatePaymentFixture fixture)
        {
            "Given a payment with card number {card}".x(() => fixture.GivenAPaymentWithCard(card));
            "When a new payment is submitted".x(fixture.WhenThePaymentIsSubmitted);
            "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
        }

        [Scenario]
        [CustomInlineAutoData("abc123")]        
        public void CreatePaymentShouldFail(string card, CreatePaymentFixture fixture)
        {
            "Given a payment with card number {card}".x(() => fixture.GivenAPaymentWithCard(card));
            "When a new payment is submitted".x(fixture.WhenThePaymentIsSubmitted);
            "Then a bad request response is returned".x(fixture.ThenABadRequestResponseIsReturned);
        }
    }
}
