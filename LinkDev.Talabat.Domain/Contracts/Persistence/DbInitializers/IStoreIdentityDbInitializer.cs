namespace LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers
{
    public interface IStoreIdentityDbInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
