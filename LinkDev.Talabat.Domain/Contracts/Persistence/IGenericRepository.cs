namespace LinkDev.Talabat.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllWithSpecsAsync(ISpecifications<TEntity, TKey> specs, bool withTracing = false);

        Task<TEntity?> GetWithSpecsAsync(ISpecifications<TEntity, TKey> specs);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
