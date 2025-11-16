using LinkDev.Talabat.Application.Abstraction.Models.Products;

namespace LinkDev.Talabat.Application.Abstraction.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(string? sort,
            int? BrandId, int? categoryId);
        Task<ProductToReturnDto> GetProductdAsync(int id);
        Task<IEnumerable<BrandDto>> GetBrandsAsync();
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();

    }
}
