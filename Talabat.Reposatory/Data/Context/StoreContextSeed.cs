using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Entities.Product;

namespace Talabat.Reposatory.Data.Context
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!context.Product_Brands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/brands.json");

                var brands = JsonSerializer.Deserialize<List<Product_Brand>>(brandsData);

                if (brands is not null && brands.Count > 0)
                {

                    foreach (var brand in brands)
                        await context.Set<Product_Brand>().AddAsync(brand);

                    await context.SaveChangesAsync();
                }
            }

            if (!context.Product_Types.Any())
            {
                var typesData = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/types.json");

                var types = JsonSerializer.Deserialize<List<Product_Type>>(typesData);

                if (types is not null && types.Count > 0)
                {

                    foreach (var type in types)
                        await context.Set<Product_Type>().AddAsync(type);

                    await context.SaveChangesAsync();
                }
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null && products.Count > 0)
                {

                    foreach (var prd in products)
                        await context.Set<Product>().AddAsync(prd);

                    await context.SaveChangesAsync();
                }
            }

            if (!context.DeliveryMethod.Any())
            {
                var deliveryMethodsData = File.ReadAllText("../Talabat.Reposatory/Data/DataSeed/delivery.json");

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods is not null && deliveryMethods.Count > 0)
                {

                    foreach (var delivery in deliveryMethods)
                        await context.Set<DeliveryMethod>().AddAsync(delivery);

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
