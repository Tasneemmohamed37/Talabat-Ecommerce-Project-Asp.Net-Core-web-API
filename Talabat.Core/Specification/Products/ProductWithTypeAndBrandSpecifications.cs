using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product;

namespace Talabat.Core.Specification.Products
{
    public class ProductWithTypeAndBrandSpecifications : BaseSpecification<Product>
    {

        public ProductWithTypeAndBrandSpecifications()
        {
            Includes.Add(P => P.Product_Type);
            Includes.Add(P => P.Product_Brand);
        }

        public ProductWithTypeAndBrandSpecifications(int id):base(P => P.Id == id)
        {
            Includes.Add(P => P.Product_Type);
            Includes.Add(P => P.Product_Brand);
        }
    }
}
