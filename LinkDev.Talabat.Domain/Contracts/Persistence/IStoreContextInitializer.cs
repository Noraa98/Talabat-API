namespace LinkDev.Talabat.Domain.Contracts.Persistence
{
    public interface IStoreContextInitializer
    {
        Task InitializeAsync();
        Task SeedAsync();
    }
}
