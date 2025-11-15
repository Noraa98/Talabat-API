using LinkDev.Talabat.Domain.Entities.Products;

namespace LinkDev.Talabat.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecefications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecefications(string sort)
            : base()
        {
            AddIncludes();

            AddSorting(sort);

        }

        public ProductWithBrandAndCategorySpecefications(int id)
            : base(id)
        {
            AddIncludes();
        }

        #region Helper Methods
        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);
        }

        private protected override void AddSorting(string sort)
        {
            switch (sort.ToLower())
            {
                case "priceAsc":
                    //OrderBy = p => p.Price;
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    //OrderByDesc = p => p.Price;
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    //OrderBy = p => p.Name;
                    AddOrderBy(p => p.Name);
                    break;
            }
        } 
        #endregion


    }
}
