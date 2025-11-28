using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Application.Mapping;
using LinkDev.Talabat.Application.Services;
using LinkDev.Talabat.Application.Services.Basket;
using LinkDev.Talabat.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(Mapper => Mapper.AddProfile(new MappingProfile()));
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            //services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) => 
            {
                /// var _mapper = serviceProvider.GetRequiredService<IMapper>();
                /// var _configuration = serviceProvider.GetRequiredService<IConfiguration>();
                /// var _basketRepository = serviceProvider.GetRequiredService<IBasketRepository>();
                /// 
                /// return () => new BasketService(_basketRepository, _mapper, _configuration);
                
                return () =>serviceProvider.GetRequiredService<IBasketService>();
            });

            return services;
        }
    }
}
