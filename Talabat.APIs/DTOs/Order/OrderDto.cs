using Talabat.APIs.DTOs.Identity;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.DTOs.Order
{
    public class OrderDto
    {
        public int DeliverMethodId { get; set; }

        public string BasketId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
