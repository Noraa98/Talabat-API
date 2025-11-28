using LinkDev.Talabat.Application.Abstraction.Services.Auth;
using LinkDev.Talabat.Application.Services.Auth;
using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.RequireUniqueEmail = true;

                // SignIn

                identityOptions.SignIn.RequireConfirmedAccount = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;


                /// identityOptions.Password.RequireNonAlphanumeric = true;
                /// identityOptions.Password.RequiredUniqueChars = 2;
                /// identityOptions.Password.RequiredLength = 6;
                /// 
                /// identityOptions.Password.RequireDigit = true;
                /// identityOptions.Password.RequireLowercase = true;
                /// identityOptions.Password.RequireUppercase = true;

                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

            })
                .AddEntityFrameworkStores<StoreIdentityContext>();

            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(Func<IAuthService>), (ServiceProvider) =>
            {
                return () => ServiceProvider.GetService<IAuthService>();
            });

            return services;
        }
    }
}
