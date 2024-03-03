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
        public ProductWithTypeAndBrandSpecifications(ProductSpecParams specParams)
            :base(P => 
                    (!specParams.BrandId.HasValue || P.Product_BrandId == specParams.BrandId) &&
                    (!specParams.TypeId.HasValue || P.Product_TypeId == specParams.TypeId)
                  ) 
        {
            Includes.Add(P => P.Product_Type);
            Includes.Add(P => P.Product_Brand);

            AddOrderBy(P => P.Name); // by defualt sort by name

            if(!string.IsNullOrEmpty(specParams.sort))
            {
                switch(specParams.sort)
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

            ApplyPagination(specParams.PageSize*(specParams.PageIndex - 1), specParams.PageSize);
        }

        public ProductWithTypeAndBrandSpecifications(int id):base(P => P.Id == id)
        {
            Includes.Add(P => P.Product_Type);
            Includes.Add(P => P.Product_Brand);
        }
    }
}
