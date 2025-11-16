namespace LinkDev.Talabat.Domain.Specifications.Product
{
    public class ProductForCountSpecification : BaseSpecifications<Entities.Products.Product, int>
    {
        public ProductForCountSpecification(int? brandId, int? CategoryId ,string? search)
        : base(P =>
                (!brandId.HasValue || P.BrandId == brandId.Value)
          &&
        (!CategoryId.HasValue || P.CategoryId == CategoryId.Value)
        &&
            (string.IsNullOrEmpty(search) ||
                P.Name.ToLower().Contains(search.ToLower()))


        )
        {
        }
    }
}