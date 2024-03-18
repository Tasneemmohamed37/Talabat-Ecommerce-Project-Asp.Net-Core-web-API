using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Cart;

namespace Talabat.APIs.DTOs.Basket
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } // string to use guid 

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

       
    }
}
