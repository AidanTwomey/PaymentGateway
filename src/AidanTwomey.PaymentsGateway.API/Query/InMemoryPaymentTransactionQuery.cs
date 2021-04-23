using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using AidanTwomey.PaymentsGateway.Domain;

namespace AidanTwomey.PaymentsGateway.API.Query
{
    public class InMemoryPaymentTransactionQuery : IPaymentTransactionQuery
    {
        private readonly ObjectCache cache;

        public InMemoryPaymentTransactionQuery(ObjectCache cache)
        {
            this.cache = cache;
        }

        public Task<PaymentTransaction> GetPayment(Guid id)
        {
            return Task.FromResult(
                cache.Get(id.ToString()) as PaymentTransaction
            );
        }
    }
}