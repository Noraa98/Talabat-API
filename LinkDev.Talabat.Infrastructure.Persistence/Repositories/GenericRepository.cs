using LinkDev.Talabat.Domain.Contracts;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext _dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracing = false)
        {
            if (typeof(TEntity) == typeof(Product))
                return withTracing ?
                      (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).ToListAsync() :
                      (IEnumerable<TEntity>)await _dbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();
            if (withTracing)
            {
                return await _dbContext.Set<TEntity>().ToListAsync();
            }
            else
            {
                return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
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
    }
}
