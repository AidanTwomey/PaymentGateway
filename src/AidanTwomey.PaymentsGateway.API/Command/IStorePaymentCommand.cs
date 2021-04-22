using System;
using System.Threading.Tasks;

namespace AidanTwomey.PaymentsGateway.API.Command
{
    public interface IStorePaymentCommand
    {
         Task StorePaymentAsync(Guid id, PaymentTransaction payment);
    }
}