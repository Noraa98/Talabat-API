using LinkDev.Talabat.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EmptySpecification<TEntity, TKey> : BaseSpecifications<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public EmptySpecification() : base()
    {
    }
}

