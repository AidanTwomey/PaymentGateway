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
            Card card = new(ValidCardNumber, 11, 2022);

            ObjectCache cache = Substitute.For<ObjectCache>();

            var command = new InMemoryCardStorageCommand(cache);

            await command.StoreCardAsync(card);

            cache.Received(1).Add(ValidCardNumber, card, Arg.Any<CacheItemPolicy>(), Arg.Any<string>());
        }
    }
}
