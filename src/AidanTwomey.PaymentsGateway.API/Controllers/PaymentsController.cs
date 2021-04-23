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

            var response = await paymentService.MakePayment(validation.ToPayment());

            await storePaymentCommand.StorePaymentAsync(
                response.Id, 
                new PaymentTransaction(
                    paymentRequest.Card, 
                    paymentRequest.Amount, 
                    DateTime.Now, 
                    response is SuccessfulPaymentResponse));

            return new CreatedResult(
                response.Id.ToString(), 
                new PaymentCreatedResponse(){Id = response.Id});
        }

        [HttpGet("v1/payments/{id}")]
        public async Task<IActionResult> GetPaymentRecord([FromRoute] Guid id)
        {
            return new OkObjectResult(await transactionQuery.GetPayment(id));
        }
    }
}
