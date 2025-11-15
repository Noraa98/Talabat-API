using LinkDev.Talabat.Domain.Specifications;

public class EmptySpecification<TEntity, TKey> : BaseSpecifications<TEntity, TKey>
    where TEntity : BaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public EmptySpecification() : base()
    {
    }
}

