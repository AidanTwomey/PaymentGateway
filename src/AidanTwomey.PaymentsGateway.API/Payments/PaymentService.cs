using System;
using System.Threading.Tasks;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IBank bank;

        public PaymentService(IBank bank)
        {
            this.bank = bank;
        }

        public async Task<PaymentResponse> MakePayment(Payment payment, Card card)
        {
            try
            {
                var bankPayment = await bank.CreatePayment(payment);
                return new SuccessfulPaymentResponse(Guid.NewGuid(), card);
            }
            catch (Refit.ApiException bankRejects)
            {
                return new FailedPaymentResponse(Guid.Empty);
            }
            catch
            {
                throw;
            }
        }
    }
}