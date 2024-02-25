using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities.Product;

namespace Talabat.APIs.Helpers
{
    public class productPictureUrlResolve : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public productPictureUrlResolve(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
                return $"{_configuration["ApiBaseUrl"]}{source.PictureURL}";
            
            return string.Empty;
        }
    }
}
