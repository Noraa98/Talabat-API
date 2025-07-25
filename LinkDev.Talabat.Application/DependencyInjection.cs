using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Mapping;
using LinkDev.Talabat.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

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

            return services;
        }
    }
}
