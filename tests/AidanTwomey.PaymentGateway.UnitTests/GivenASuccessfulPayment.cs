using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Command;
using AidanTwomey.PaymentsGateway.API.Payments;
using AidanTwomey.PaymentsGateway.API.Validation;
using AidanTwomey.PaymentsGateway.Domain;
using NSubstitute;
using Shouldly;
using Xunit;

namespace AidanTwomey.PaymentGateway.UnitTests
{
    public class GivenASuccessfulPayment
    {
        private const string ValidCardNumber = "4716887383567911";
        
        [Fact]
        public async Task AndAnInMemoryPaymentStore_When_StorePaymentAsync_Then_AddToCache()
        {
            const string transactionId = "d07840a0-279e-433f-a8b0-7760b5929153";

            Card card = new(ValidCardNumber, 11, 2022);
            PaymentTransaction transaction = new(card, 9.99m, new DateTime(2021,4,22,19,27,0), true);

            ObjectCache cache = Substitute.For<ObjectCache>();

            var command = new InMemoryStorePaymentCommand(cache);

            await command.StorePaymentAsync(
                Guid.Parse(transactionId),
                transaction 
                );

            cache.Received(1).Add(transactionId, transaction, Arg.Any<CacheItemPolicy>(), Arg.Any<string>());
        }

        [Fact]
        public void When_Map_ToResponse_Then_CardDetailIsMasked()
        {
            Guid id = Guid.NewGuid();

            const string Number = "4485710512780511";

            var response = new SuccessfulPaymentResponse(id, new Card(Number, 11, 2023)) {};

            var paymentCreatedResponse = response.ToResponse();

            paymentCreatedResponse.Id.ShouldBe(id);
            paymentCreatedResponse.CardNumber.ShouldBe("************0511");
            
        }
    }
}
