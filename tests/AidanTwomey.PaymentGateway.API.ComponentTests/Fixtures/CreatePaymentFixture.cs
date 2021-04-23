using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using AidanTwomey.PaymentsGateway.API;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Validation;
using System.Runtime.Caching;
using AidanTwomey.PaymentsGateway.Domain;
using AidanTwomey.PaymentsGateway.API.Query;
using Newtonsoft.Json;
using AidanTwomey.PaymentsGateway.API.Payments;

namespace AidanTwomey.PaymentGateway.API.ComponentTests.Fixtures
{
    public class CreatePaymentFixture : ApiFixture<Startup>
    {
        private const string TransactionId = "1261b691-f8aa-4e84-900f-9cdc0e2c5c0c";
        readonly Guid userRestaurantId = Guid.Parse("2AA18D86-1A4C-4305-95A7-912C7C0FC5E1");
        private string cardNumber;

        public CreatePaymentFixture()
        {
        }

        protected override void RegisterDependencies(IServiceCollection collection)
        {
            collection.AddSingleton<IPaymentTransactionQuery, InMemoryPaymentTransactionQuery>();

            collection.AddTransient(IoC =>
            {
                ObjectCache objectCache = Substitute.For<ObjectCache>();
                objectCache
                    .Get(TransactionId, Arg.Any<string>())
                    .Returns(new PaymentTransaction(new Card("4916690086480301", 12, 2022), 19.99m, DateTime.Now, true ));
                return objectCache;
            });
            collection.AddTransient<IPaymentValidator, PaymentValidator>();
            collection.AddTransient<IPaymentService, PaymentService>();
            collection.AddTransient<IBank>(_ =>
            {
                IBank bank = Substitute.For<IBank>();
                bank.CreatePayment(Arg.Any<Payment>()).Returns(Task.FromResult(new ProcessedPayment(){success = true}));
                return bank;
            });
            
        }

        /****** GIVEN ******************************************************/

        internal void GivenAPaymentWithCard(string card)
        {
            this.cardNumber = card;
        }

        internal void GivenAPaymentExists()
        {
        }

        /****** WHEN *******************************************************/

        internal async Task WhenThePaymentIsSubmitted()
        {
            await CreatePayment(new MakePaymentRequest()
            {
                Card = new(cardNumber, 12,2021),
                Amount = 9.99m
            });
        }

        internal async Task<HttpResponseMessage> WhenThePaymentIsRetrieved()
        {
            return await SendAsync(HttpMethod.Get, $"/v1/payments/{TransactionId}");
        }

        public async Task<HttpResponseMessage> CreatePayment(MakePaymentRequest payment)
        {
            return await SendAsync(HttpMethod.Post, $"/v1/payments", payment);
        }

        // /****** THEN ******************************************************/

        internal void ThenASuccessfulResponseIsReturned()
        {
            LastResponse.IsSuccessStatusCode.ShouldBeTrue();
        }

        internal void ThenABadRequestResponseIsReturned()
        {
            LastResponse.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
        }

        internal async Task AndTheTransactionWasASuccess()
        {
            var body = await LastResponse.Content.ReadAsStringAsync();
            
            var transaction = new { Success = default(bool) };
            transaction = JsonConvert.DeserializeAnonymousType(body, transaction);

            transaction.Success.ShouldBeTrue();
        }
    }
}
