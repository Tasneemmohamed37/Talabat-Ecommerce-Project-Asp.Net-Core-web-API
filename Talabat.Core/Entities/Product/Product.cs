using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Product
{
    public class Product :BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Product_Brand")]
        public int Product_BrandId { get; set; }
        public Product_Brand Product_Brand { get; set; } // nav one


        [ForeignKey("Product_Type")]
        public int Product_TypeId { get; set; }
        public Product_Type Product_Type { get; set; } // nav one
    }
}
