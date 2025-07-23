using LinkDev.Talabat.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
            if (spec.Criteria != null) // P=> P.Id.Equals(1)
                query = query.Where(spec.Criteria);
            // query = query.Where(spec.Criteria); // P=> P.Id.Equals(1)

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression)); // P=> P.Brand
            
            return inputQuery;
        }
    }
}
