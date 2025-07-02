using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Contracts
{
    public interface IGenericRepository<TEntity ,TKey>
        where TEntity : BaseEntity<TKey>
        where TKey: IEquatable<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracing = false);
        Task<TEntity?> GetAsync(TKey id);

        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void DeleteAsync(TEntity entity);
    }
}
