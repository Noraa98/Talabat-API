using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Common;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Specifications.Product;
using LinkDev.Talabat.Domain.Specifications.Products;

namespace LinkDev.Talabat.Application.Services.Products
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var specs = new ProductWithBrandAndCategorySpecefications(specParams.Sort, specParams.BrandId, specParams.CategoryId
                , specParams.PageIndex , specParams.PageSize ,specParams.Search);



            var products = await unitOfWork.GetRepository<Product, int>().GetAllWithSpecsAsync(specs);

            var countSpecs = new ProductForCountSpecification(specParams.BrandId, specParams.CategoryId ,specParams.Search);

            var count = await unitOfWork
                .GetRepository<Product, int>()
                .CountAsync(countSpecs);

            var data = mapper.Map<IEnumerable<ProductToReturnDto>>(products);


            return new Pagination<ProductToReturnDto>(specParams.PageIndex,specParams.PageSize , count) { Data = data };
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
