using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIs.DTOs.Basket;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Cart;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger _logger;

        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        private const string _webhookSecret = "whsec_5105e7e442206048996e99acdb95fe006db5b41a8eec2723f87dc1fcfbea0c8b";


        public PaymentsController(
            IPaymentService paymentService,
            ILogger logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        #region Create Or Update PaymentIntent
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null)
                return BadRequest(new ApiResponse(400, "A Problem With Your Basket"));



            return Ok(basket);
        }
        #endregion


        #region Webhook
        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
           
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
           
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _webhookSecret);

                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                Order order;

                // Handle the event
                switch (stripeEvent.Type)
                {
                    case Events.PaymentIntentSucceeded:
                        order = await _paymentService.UpdatePaymentIntentToSuccessOrFailed(paymentIntent.Id, true);
                        _logger.LogInformation("payment is succeded", paymentIntent.Id);
                        break;
                    case Events.PaymentIntentPaymentFailed:
                        order = await _paymentService.UpdatePaymentIntentToSuccessOrFailed(paymentIntent.Id, false);
                        _logger.LogInformation("payment is failed", paymentIntent.Id);
                        break;
                }
               
                return Ok();
           
        }
        #endregion

    }
}
