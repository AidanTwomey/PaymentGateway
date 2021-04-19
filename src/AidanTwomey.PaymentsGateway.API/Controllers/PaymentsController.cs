using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AidanTwomey.PaymentGateway.API.Model;

namespace AidanTwomey.Paymentsgateway.API.Controllers
{

    // [Consumes("application/json")]
    // [Produces("application/json")]
    // [ApiExplorerSettings(GroupName = "Category")]
    public class PaymentsController
    {
        // readonly ICommandHandler<CreateCategory, Guid> commandHandler;

        // public AddMenuCategoryController(ICommandHandler<CreateCategory, Guid> commandHandler)
        // {
        //     this.commandHandler = commandHandler;
        // }

        [HttpPost("v1/payments")]
        // [Authorize]
        // [ProducesResponseType(typeof(ResourceCreatedResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> MakePayment([FromBody]MakePaymentRequest body)
        {
            // NOTE: Please ensure the API returns the response codes annotated above

            // var categoryId = await commandHandler.HandleAsync(
            //     new CreateCategory(
            //         correlationId: GetCorrelationId(),
            //         menuId: id,
            //         name: body.Name,
            //         description: body.Description
            //     )
            // );

            // return StatusCode(StatusCodes.Status201Created, new ResourceCreatedResponse(categoryId));
            return new StatusCodeResult(201);
        }
    }
}
