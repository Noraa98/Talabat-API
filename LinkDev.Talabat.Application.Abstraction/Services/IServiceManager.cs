using LinkDev.Talabat.Application.Abstraction.Services.Products;

namespace LinkDev.Talabat.Application.Abstraction.Services
{
    public interface IServiceManager
    {
        public IProductService ProductService { get; }

    }
}
