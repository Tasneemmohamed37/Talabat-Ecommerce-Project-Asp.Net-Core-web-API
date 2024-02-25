using System.ComponentModel.DataAnnotations.Schema;
using Talabat.Core.Entities.Product;

namespace Talabat.APIs.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureURL { get; set; }
        public decimal Price { get; set; }

        public int Product_BrandId { get; set; }
        public string Product_Brand { get; set; }

        public int Product_TypeId { get; set; }
        public string Product_Type { get; set; } 
    }
}
