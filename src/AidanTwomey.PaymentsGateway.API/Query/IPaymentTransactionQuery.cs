using System;
using System.Threading.Tasks;
using AidanTwomey.PaymentsGateway.Domain;

namespace AidanTwomey.PaymentsGateway.API.Query
{
    public interface IPaymentTransactionQuery
    {
         Task<PaymentTransaction> GetPayment(Guid id);
    }
}