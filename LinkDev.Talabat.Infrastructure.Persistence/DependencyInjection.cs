using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services
            , IConfiguration configuration)
        {
            #region Store Context

            services.AddDbContext<StoreContext>(optionsBuilder =>
               {
                   optionsBuilder
                       .UseLazyLoadingProxies() // Enable Lazy Loading
                       .UseSqlServer(configuration.GetConnectionString("StoreContext"));

               });
            services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();
            services.AddScoped(typeof(IStoreContextInitializer), typeof(StoreContextInitializer));

            #endregion

            #region Identity Context

            services.AddDbContext<StoreIdentityContext>(optionsBuilder =>
            {
                optionsBuilder
                    .UseLazyLoadingProxies() // Enable Lazy Loading
                    .UseSqlServer(configuration.GetConnectionString("IdentityContext"));

            });

            services.AddScoped<IStoreIdentityDbInitializer, StoreIdentityDbInitializer>();

            #endregion

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            


            return services;
        }
    }
}
