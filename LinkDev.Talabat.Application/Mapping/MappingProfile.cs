using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Domain.Entities.Basket;
using LinkDev.Talabat.Domain.Entities.Products;

namespace LinkDev.Talabat.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Brand, o => o.MapFrom(src => src.Brand!.Name))
                .ForMember(d => d.Category, o => o.MapFrom(src => src.Category!.Name))
                //.ForMember(d => d.Category, o => o.MapFrom(src => $"{"https://localhost:7101"} {src.PictureUrl}"));
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand,BrandDto>();
            CreateMap<ProductCategory,CategoryDto>();

            CreateMap<Basket , BasketDto>().ReverseMap();
            CreateMap<BasketItem , BasketItemDto>()
                .ForMember(d => d.Id, o => o.MapFrom(src => src.ProductId))
                .ReverseMap()
                .ForMember(d => d.ProductId, o => o.MapFrom(src => src.Id));
        }
      
    }

}
