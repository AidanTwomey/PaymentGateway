using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
// using Amido.Stacks.Application.CQRS.ApplicationEvents;
using Amido.Stacks.Testing.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using AidanTwomey.PaymentsGateway.API;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Validation;
using System.Runtime.Caching;

namespace AidanTwomey.PaymentGateway.API.ComponentTests.Fixtures
{
    public class CreatePaymentFixture : ApiFixture<Startup>
    {
        readonly Guid userRestaurantId = Guid.Parse("2AA18D86-1A4C-4305-95A7-912C7C0FC5E1");
        private string cardNumber;

        public CreatePaymentFixture()
        {
        }

        protected override void RegisterDependencies(IServiceCollection collection)
        {
            collection.AddTransient(IoC => Substitute.For<ObjectCache>());
            collection.AddTransient(IoC => Substitute.For<IPaymentValidator>());
        }

        /****** GIVEN ******************************************************/

        /****** WHEN *******************************************************/

        internal async Task WhenThePaymentIsSubmitted()
        {
            await CreatePayment(new MakePaymentRequest(){Card = new(cardNumber, 12,2021)});
        }

        public async Task<HttpResponseMessage> CreatePayment(MakePaymentRequest payment)
        {
            return await SendAsync(HttpMethod.Post, $"/v1/payments", payment);
        }

        internal void AndTheCardNumberIs(string card)
        {
            this.cardNumber = card;
        }

        // /****** THEN ******************************************************/

        internal void ThenASuccessfulResponseIsReturned()
        {
            LastResponse.IsSuccessStatusCode.ShouldBeTrue();
        }
    }
}
