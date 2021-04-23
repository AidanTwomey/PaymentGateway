using System;
using System.Threading.Tasks;

namespace AidanTwomey.PaymentsGateway.API.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IBank bank;

        public PaymentService(IBank bank)
        {
            this.bank = bank;
        }

        public async Task<PaymentResponse> MakePayment(Payment request)
        {
            var payment = await bank.CreatePayment(request);

            return payment.success
                ? new SuccessfulPaymentResponse(Guid.NewGuid(), request.Card)
                : new FailedPaymentResponse(Guid.Empty) as PaymentResponse;
        }
    }
}