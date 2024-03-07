using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Cart;

namespace Talabat.Core.Interfaces
{
    public interface IBasketRepository // will not inhiriate from genericRepo 'which treat with context' but this repo will treat with redis
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);

        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);

        Task<bool> DeleteBasketAsync(string basketId);
    }
}
