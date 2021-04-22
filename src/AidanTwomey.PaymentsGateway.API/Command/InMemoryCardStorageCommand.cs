using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Command
{
    public class InMemoryCardStorageCommand : ICardStorageCommand
    {
        private readonly ObjectCache cache;
        private readonly CacheItemPolicy cachePolicy = new CacheItemPolicy(){SlidingExpiration = new TimeSpan(1,0,0)};

        public InMemoryCardStorageCommand(ObjectCache cache)
        {
            this.cache = cache;
        }

        public Task StoreCardAsync(Card card)
        {
            cache.Add(card.Number, card, cachePolicy);

            return Task.CompletedTask;
        }
    }
}