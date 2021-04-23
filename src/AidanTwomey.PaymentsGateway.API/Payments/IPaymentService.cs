using System.Threading.Tasks;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public interface IPaymentService
    {
         Task<PaymentResponse> MakePayment(Payment request);
    }
}