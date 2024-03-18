using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Cart
{
    public class BasketItem : BaseEntity
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Product_Brand { get; set; }
        public string Product_Type { get; set; }
    }
}
