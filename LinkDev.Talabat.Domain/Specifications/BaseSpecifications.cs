using LinkDev.Talabat.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity , bool>>? Criteria { get; set ; } = null;
        
       public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();

        // constructor to initialize the criteria and includes if needed
        public BaseSpecifications()
        {
            // Criteria = null;
        }
        public BaseSpecifications(TKey id)
        {
            Criteria = E => E.Id.Equals(id);
        }


        #region Helper Methods
        private protected virtual void AddIncludes()
        {
        }
        #endregion
    }
}
