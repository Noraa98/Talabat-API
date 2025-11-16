using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllWithSpecsAsync(ISpecifications<TEntity, TKey> specs , bool withTracing = false)
        {
            return await ApplySpecifications(specs).ToListAsync();
        }

        public async Task<TEntity?> GetWithSpecsAsync(ISpecifications<TEntity, TKey> specs)
        {
            return await ApplySpecifications(specs).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specs)
        {
            return await ApplySpecifications(specs).CountAsync();
        }


        public async Task AddAsync(TEntity entity)
        {
             await _dbContext.Set<TEntity>().AddAsync(entity);
        }
        public Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return Task.CompletedTask; // No need to await as EF Core tracks changes automatically
        }
        public void DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);

        }

        #region Helpers

        private IQueryable<TEntity> ApplySpecifications( ISpecifications<TEntity, TKey> specs)
        {
            return SpecificationsEvaluator.GetQuery<TEntity, TKey>(_dbContext.Set<TEntity>(), specs);
        }
        #endregion
    }
}
