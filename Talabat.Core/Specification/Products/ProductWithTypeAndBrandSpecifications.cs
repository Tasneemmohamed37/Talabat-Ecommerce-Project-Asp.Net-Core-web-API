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

        public ProductWithTypeAndBrandSpecifications(string? sort, int? brandId, int? typeId)
            :base(P => 
                    (!brandId.HasValue || P.Product_BrandId == brandId) &&
                    (!typeId.HasValue || P.Product_TypeId == typeId)
                  ) 
        {
            Includes.Add(P => P.Product_Type);
            Includes.Add(P => P.Product_Brand);

            AddOrderBy(P => P.Name); // by defualt sort by name

            if(!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                };
            }
        }

        public ProductWithTypeAndBrandSpecifications(int id):base(P => P.Id == id)
        {
            Includes.Add(P => P.Product_Type);
            Includes.Add(P => P.Product_Brand);
        }
    }
}
