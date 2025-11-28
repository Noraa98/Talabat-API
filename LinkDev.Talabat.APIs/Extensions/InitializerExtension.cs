using LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers;

namespace LinkDev.Talabat.APIs.Extensions
{
    public static class InitializerExtension
    {
        public static async Task<WebApplication> InitializeStoreContextAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateAsyncScope();
            var services = scope.ServiceProvider;

            var storeContextInitializer = services.GetRequiredService<IStoreContextInitializer>();
            var IdentityDbInitializer = services.GetRequiredService<IStoreIdentityDbInitializer>();


            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await storeContextInitializer.InitializeAsync();
                await storeContextInitializer.SeedAsync(); // Seed Data to the Database


                await IdentityDbInitializer.InitializeAsync();
                await IdentityDbInitializer.SeedAsync(); // Seed Data to the Database

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred during apply yhe migration.");
            }

            return app;

        }
    }
}
