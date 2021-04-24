using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public interface IPaymentService
    {
         Task<PaymentResponse> MakePayment(Payment payment, Card card);
    }
}