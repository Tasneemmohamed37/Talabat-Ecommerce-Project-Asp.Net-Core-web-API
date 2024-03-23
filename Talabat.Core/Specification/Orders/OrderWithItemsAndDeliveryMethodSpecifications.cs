using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Specification.Orders
{
    public class OrderWithItemsAndDeliveryMethodSpecifications : BaseSpecification<Order>
    {
        public OrderWithItemsAndDeliveryMethodSpecifications(string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail)
        {
            Includes.Add(O => O.Items); //Although items is nav prop [many] but will load it using eager loading, because each time you ask order must include items 
            Includes.Add(O => O.DeliveryMethod);

            AddOrderByDesc(O => O.OrderDate);
        }


        public OrderWithItemsAndDeliveryMethodSpecifications(int orderId, string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail && O.Id == orderId)
        {
            Includes.Add(O => O.Items); //Although items is nav prop [many] but will load it using eager loading, because each time you ask order must include items 
            Includes.Add(O => O.DeliveryMethod);
        }
    }
}
