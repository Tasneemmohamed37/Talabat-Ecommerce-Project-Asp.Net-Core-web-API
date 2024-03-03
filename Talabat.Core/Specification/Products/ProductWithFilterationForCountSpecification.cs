using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Product;

namespace Talabat.Core.Specification.Products
{
    public class ProductWithFilterationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterationForCountSpecification(ProductSpecParams specParams)
            : base(P =>
                    (!specParams.BrandId.HasValue || P.Product_BrandId == specParams.BrandId) &&
                    (!specParams.TypeId.HasValue || P.Product_TypeId == specParams.TypeId)
                  )
        {
            
        }
    }
}
