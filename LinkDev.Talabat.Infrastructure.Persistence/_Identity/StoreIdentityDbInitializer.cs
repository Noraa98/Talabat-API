using LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers;
using LinkDev.Talabat.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence._Identity
{
    public class StoreIdentityDbInitializer : IStoreIdentityDbInitializer
    {
        private readonly StoreIdentityContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public StoreIdentityDbInitializer(StoreIdentityContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }


        public async Task InitializeAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync();// Apply Migrations if any [UPdate Database Schema]

        }

        public async Task SeedAsync()
        {
            if(!_userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Noura Ahmed",
                    UserName = "noura.ahmed",
                    Email = "noura@gmail.com",
                    PhoneNumber = "1234567890",
                };
                await _userManager.CreateAsync(user, "P@ssw0rd");
            }
        }
    }
}