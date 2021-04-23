using Xbehave;
using AidanTwomey.PaymentGateway.API.ComponentTests.Fixtures;

namespace AidanTwomey.PaymentGateway.API.ComponentTests.Features
{
    public class GetPaymentFeature
    {
        [Scenario]
        [CustomInlineAutoData("4716144209705113")]        
        public void GetPaymentShouldSucceed(string card, CreatePaymentFixture fixture)
        {
            "Given a payment exists".x(fixture.GivenAPaymentExists);
            "When we get the Payments API".x(fixture.WhenThePaymentIsRetrieved);
            "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
            "And the returned transaction was a success".x(fixture.AndTheTransactionWasASuccess);
        }
    }
}
