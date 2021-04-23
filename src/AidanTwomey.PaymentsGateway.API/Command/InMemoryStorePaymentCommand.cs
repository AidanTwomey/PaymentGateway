using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using AidanTwomey.PaymentsGateway.Domain;

namespace AidanTwomey.PaymentsGateway.API.Command
{
    public class InMemoryStorePaymentCommand : IStorePaymentCommand
    {
        private readonly ObjectCache cache;
        private readonly CacheItemPolicy cachePolicy = new CacheItemPolicy(){SlidingExpiration = new TimeSpan(1,0,0)};

        public InMemoryStorePaymentCommand(ObjectCache cache)
        {
            this.cache = cache;
        }

        public Task StorePaymentAsync(Guid id, PaymentTransaction payment)
        {
            cache.Add(id.ToString(), payment, cachePolicy);

            return Task.CompletedTask;
        }
    }
}