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
            object payment = cache.Get(id.ToString());

            var transaction = (payment == null)
                ? new NotFoundTransaction()
                : payment as PaymentTransaction;
            
            return Task.FromResult(transaction);
        }
    }
}