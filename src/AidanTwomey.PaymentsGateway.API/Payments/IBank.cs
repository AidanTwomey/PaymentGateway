using System.Threading.Tasks;
using Refit;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public interface IBank
    {
        [Post("/payment")]
        Task<ProcessedPayment> CreatePayment(Payment payment);
    }
}