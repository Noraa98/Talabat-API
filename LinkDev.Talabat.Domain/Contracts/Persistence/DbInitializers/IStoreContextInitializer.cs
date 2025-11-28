namespace LinkDev.Talabat.Domain.Contracts.Persistence.DbInitializers
{
    public interface IStoreContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
