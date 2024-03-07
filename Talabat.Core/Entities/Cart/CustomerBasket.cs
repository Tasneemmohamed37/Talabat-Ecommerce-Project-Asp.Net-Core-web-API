using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Cart
{
    public class CustomerBasket
    {
        public string Id { get; set; } // string to use guid 

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
