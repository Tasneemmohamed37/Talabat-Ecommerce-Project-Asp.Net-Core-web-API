using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now; // use dateTimeOffset becouse if it international website must use UTC universal time 
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShipToAddress { get; set; } // prop inside address will mapped to columns in db  
        public DeliveryMethod DeliveryMethod { get; set; } // Nav Prop [ONE]
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); // Nav Prop [Many]

        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal SubTotal { get; set; }

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;
    }
}
