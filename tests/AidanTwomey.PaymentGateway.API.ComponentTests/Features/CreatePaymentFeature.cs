using Xbehave;
using AidanTwomey.PaymentGateway.API.ComponentTests.Fixtures;

namespace AidanTwomey.PaymentGateway.API.ComponentTests.Features
{
    public class CreatePaymentFeature
    {
        /* SCENARIOS: Create a category in the menu
          
             Examples: 
             ---------------------------------------------------------------------------------
            | AsRole              | Existing Menu | Existing Category  | Outcome              |
            |---------------------|---------------|--------------------|----------------------|
            | Admin               | Yes           | No                 | 200 OK               |
            | Employee            | Yes           | No                 | 200 OK               |
            | Admin               | No            | No                 | 404 Not Found        |
            | Admin               | Yes           | Yes                | 409 Conflict         |
            | Customer            | Yes           | No                 | 403 Forbidden        |
            | UnauthenticatedUser | Yes           | No                 | 403 Forbidden        |

        */

        [Scenario]
        [CustomInlineAutoData("4716144209705113")]        
        public void CreateCategoryShouldSucceed(string card, CreatePaymentFixture fixture)
        {
            "When a new payment is submitted".x(fixture.WhenThePaymentIsSubmitted);
            "And the payment has card number {card}".x(() => fixture.AndTheCardNumberIs(card));
            "Then a successful response is returned".x(fixture.ThenASuccessfulResponseIsReturned);
        }
    }
}
