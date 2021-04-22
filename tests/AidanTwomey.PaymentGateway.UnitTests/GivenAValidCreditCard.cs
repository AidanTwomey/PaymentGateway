using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Command;
using NSubstitute;
using Xunit;

namespace AidanTwomey.PaymentGateway.UnitTests
{
    public class GivenAValidCreditCard
    {
        private const string ValidCardNumber = "4716887383567911";
        
        [Fact]
        public async Task AndAnInMemoryCardStore_When_StoreCardAsync_Then_AddToCache(
        )
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
    }
}
