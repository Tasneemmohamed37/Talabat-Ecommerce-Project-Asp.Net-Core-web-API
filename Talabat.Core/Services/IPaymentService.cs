using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Cart;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Services
{
    public interface IPaymentService
    {
       Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

       Task<Order> UpdatePaymentIntentToSuccessOrFailed(string paymentIntentId, bool isSucceded);
    }
}
