using System.Threading.Tasks;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public interface IPaymentService
    {
         Task<PaymentResponse> MakePayment(Payment request);
    }

    public class PaymentService : IPaymentService
    {
        public Task<PaymentResponse> MakePayment(Payment request)
        {
            return Task.FromResult(new PaymentResponse());
        }
    }
}