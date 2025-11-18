using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Services.Products;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Application.Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<IBasketService> _basketService;


        public ServiceManager(IUnitOfWork unitOfWork , IMapper mapper, IConfiguration configuration ,Func<IBasketService> basketServiceFactory )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productService = new Lazy<IProductService>(() => new ProductService(_unitOfWork, _mapper));
            _basketService = new Lazy<IBasketService>(basketServiceFactory);
        }
        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;





        // project flow: Controller → ServiceManager → ProductService → UnitOfWork → GenericRepository → Specification → DbContext → Database → DTOs


    }
}
