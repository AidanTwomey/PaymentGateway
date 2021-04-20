using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AidanTwomey.PaymentGateway.API.Model;
using AidanTwomey.PaymentsGateway.API.Validation;
using AidanTwomey.PaymentsGateway.API.Command;
using Microsoft.AspNetCore.Http;
using AidanTwomey.PaymentsGateway.API.Payments;

namespace AidanTwomey.Paymentsgateway.API.Controllers
{
    // [Consumes("application/json")]
    // [Produces("application/json")]
    public class PaymentsController
    {
        private readonly IPaymentValidator paymentValidator;
        private readonly ICardStorageCommand cardStorageCommand;
        private readonly IPaymentService paymentService;

        public PaymentsController(
            IPaymentValidator paymentValidator, 
            ICardStorageCommand cardStorageCommand,
            IPaymentService paymentService)
        {
            this.paymentValidator = paymentValidator;
            this.cardStorageCommand = cardStorageCommand;
            this.paymentService = paymentService;
        }

        [HttpPost("v1/payments")]
        // [Authorize]
        [ProducesResponseType(typeof(ResourceCreatedResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> MakePayment([FromBody] MakePaymentRequest paymentRequest)
        {
            var validation = this.paymentValidator.Validate(paymentRequest);

            if (validation is InvalidPayment)
                return new BadRequestResult();

            await cardStorageCommand.StoreCardAsync();

            var response = await paymentService.MakePayment(validation.ToPayment());

            return new CreatedResult(response.Id.ToString(), new ResourceCreatedResponse());
        }
    }
}
