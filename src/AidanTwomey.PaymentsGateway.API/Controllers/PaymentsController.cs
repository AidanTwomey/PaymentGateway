using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Validation;
using AidanTwomey.PaymentsGateway.API.Command;
using Microsoft.AspNetCore.Http;
using AidanTwomey.PaymentsGateway.API.Payments;
using System;
using AidanTwomey.PaymentsGateway.Domain;
using AidanTwomey.PaymentsGateway.API.Query;

namespace AidanTwomey.Paymentsgateway.API.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PaymentsController
    {
        private readonly IPaymentValidator paymentValidator;
        private readonly IStorePaymentCommand storePaymentCommand;
        private readonly IPaymentTransactionQuery transactionQuery;
        private readonly IPaymentService paymentService;

        public PaymentsController(
            IPaymentValidator paymentValidator,
            IStorePaymentCommand cardStorageCommand,
            IPaymentTransactionQuery transactionQuery,
            IPaymentService paymentService)
        {
            this.paymentValidator = paymentValidator;
            this.storePaymentCommand = cardStorageCommand;
            this.transactionQuery = transactionQuery;
            this.paymentService = paymentService;
        }

        [HttpPost("v1/payments")]
        // [Authorize]
        [ProducesResponseType(typeof(PaymentCreatedResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> MakePayment([FromBody] MakePaymentRequest paymentRequest)
        {
            var validation = this.paymentValidator.Validate(paymentRequest);

            if (validation is InvalidPayment)
                return new BadRequestResult();

            var payment = await paymentService.MakePayment(validation.ToPayment(), validation.Transaction.Card);

            if (payment.Rejected)
                return new BadRequestResult();

            await storePaymentCommand.StorePaymentAsync(payment.Id, validation.Transaction);

            return new CreatedResult(
                payment.Id.ToString(),
                payment.ToResponse());
        }

        [HttpGet("v1/payments/{id}")]
        public async Task<IActionResult> GetPaymentRecord([FromRoute] Guid id)
        {
            var transaction = await transactionQuery.GetPayment(id);

            return new OkObjectResult(new 
            { 
                CardNumber = transaction.Card.ToString(), 
                Timestamp = transaction.Timestamp, 
                Amount = transaction.Amount,
                Success = transaction.Success 
            });
        }
    }
}
