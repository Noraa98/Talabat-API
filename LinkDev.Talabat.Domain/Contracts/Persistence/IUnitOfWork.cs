namespace LinkDev.Talabat.Domain.Contracts.Persistence
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        // Get generic repository
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>;

        // Save changes
        Task<int> CompleteAsync(); // = await _dbContext.SaveChangesAsync();
    }

}
