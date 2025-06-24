using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer("StoreContext");
            });
            return services;
        }
    }
}
