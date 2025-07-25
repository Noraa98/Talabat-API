using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(string sort)
        {
            var specs = new ProductWithBrandAndCategorySpecefications(sort);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecsAsync(specs);
            var productsToReturn = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return productsToReturn;
        }
        public async Task<ProductToReturnDto> GetProductdAsync(int id)
        {
            var specs = new ProductWithBrandAndCategorySpecefications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetWithSpecsAsync(specs);
            var productToReturn = mapper.Map<ProductToReturnDto>(product);
            return productToReturn;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var spec = new EmptySpecification<ProductBrand, int>();
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllWithSpecsAsync(spec);
            return mapper.Map<IEnumerable<BrandDto>>(brands);
        }


        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var spec = new EmptySpecification<ProductCategory, int>();
            var categories = await unitOfWork.GetRepository<ProductCategory, int>().GetAllWithSpecsAsync(spec);
            return mapper.Map<IEnumerable<CategoryDto>>(categories);
        }



    }
}
