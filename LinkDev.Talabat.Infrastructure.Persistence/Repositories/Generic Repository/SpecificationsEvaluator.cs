using LinkDev.Talabat.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository
{
    internal static class SpecificationsEvaluator //<TEntity, TKey>
    //    where TEntity : BaseEntity<TKey>
    //    where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
        { 
            var query = inputQuery; // _dbContext.Set<Product>()
            
            // WHERE
            if (spec.Criteria != null) // P=> P.Id.Equals(1)
                query = query.Where(spec.Criteria);
            // query = query.Where(spec.Criteria); // P=> P.Id.Equals(1)

            // ORDER BY
            if (spec.OrderBy != null) // P=> P.Id
                query = query.OrderBy(spec.OrderBy);

            // ORDER BY DESC
            else if (spec.OrderByDesc != null) // P=> P.Id
                query = query.OrderByDescending(spec.OrderByDesc);

            // PAGING
            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);


            // INCLUDEs
            query = spec.Includes.Aggregate(
                query,
                (currentQuery, includeExpression) => currentQuery.Include(includeExpression)
            ); // P=> P.Brand
            
            return query;
        }
    }
}
