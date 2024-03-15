using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.APIs.DTOs.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Product;

namespace Talabat.APIs.Helpers
{
    public class mappingProfiles : Profile
    {
        public mappingProfiles()
        {
            //forMemebr(destination , O => O.MapFrom(source))

            #region Product

            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Product_Brand, O => O.MapFrom(S => S.Product_Brand.Name))
                .ForMember(d => d.Product_Type, O => O.MapFrom(S => S.Product_Type.Name))
                .ForMember(d => d.PictureURL , O => O.MapFrom<productPictureUrlResolve>());
            #endregion

            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
