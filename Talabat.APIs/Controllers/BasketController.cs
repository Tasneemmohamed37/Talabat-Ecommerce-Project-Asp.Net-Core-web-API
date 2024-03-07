using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Cart;
using Talabat.Core.Interfaces;

namespace Talabat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        #region GetBasketAsync

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id); // in case basket not found becouse expire date , will create new basket with same old id
        }

        #endregion

        #region CreateOrUpdateBasket

        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasket basket)
        {
            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            if (createdOrUpdatedBasket != null)
                return Ok(createdOrUpdatedBasket);
            return BadRequest(new ApiResponse(400));
        }

        #endregion

        #region DeleteBasket

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        } 
        #endregion
    }
}
