using LinkDev.Talabat.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _repositories= new ();

        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
           where TEntity : BaseEntity<TKey>
           where TKey : IEquatable<TKey>
        {
            ///return new GenericRepository<TEntity,TKey>(_dbContext);

            /// var typeName = typeof(TEntity).Name; //Product
            /// if(_repositories.ContainsKey(typeName)) return (IGenericRepository< TEntity, TKey>) _repositories[typeName];
            /// 
            /// var repository = new GenericRepository<TEntity, TKey>(_dbContext);
            /// 
            /// _repositories.Add(typeName, repository);
            /// return repository;
            /// 

            return (IGenericRepository<TEntity, TKey>)_repositories
                .GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_dbContext));


        }
        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

       
    }
}
