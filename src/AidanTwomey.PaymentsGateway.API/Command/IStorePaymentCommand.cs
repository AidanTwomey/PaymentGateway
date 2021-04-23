using System;
using System.Threading.Tasks;
using AidanTwomey.PaymentsGateway.Domain;

namespace AidanTwomey.PaymentsGateway.API.Command
{
    public interface IStorePaymentCommand
    {
         Task StorePaymentAsync(Guid id, PaymentTransaction payment);
    }
}