using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Application.Mapping
{

    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string?>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string? Resolve(Product source, ProductToReturnDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_configuration["Urls:ApiBaseUrl"]}/{source.PictureUrl}";

            return string.Empty;
        }
    }

}
