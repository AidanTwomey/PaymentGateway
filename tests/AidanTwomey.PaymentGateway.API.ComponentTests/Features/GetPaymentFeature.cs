using Xbehave;
using AidanTwomey.PaymentGateway.API.ComponentTests.Fixtures;
using AutoFixture.Xunit2;

namespace AidanTwomey.PaymentGateway.API.ComponentTests.Features
{
    public class GetPaymentFeature
    {
        [Scenario]
        [InlineAutoDataAttribute]        
        public void GetPaymentShouldSucceed(CreatePaymentFixture fixture)
        {
            "Given a payment exists".x(fixture.GivenAPaymentExists);
            "When we get the Payments API".x(fixture.WhenThePaymentIsRetrieved);
            "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
            "And the returned transaction was a success".x(fixture.AndTheTransactionWasASuccess);
        }

        [Scenario]
        [InlineAutoDataAttribute]        
        public void GetPaymentShouldFail(CreatePaymentFixture fixture)
        {
            "Given a payment does not exist".x(fixture.GivenAPaymentDoesNotExist);
            "When we get the Payments API".x(fixture.WhenThePaymentIsRetrieved);
            "Then a not found response is returned".x(fixture.ThenANotFoundResponseIsReturned);
        }
    }
}
