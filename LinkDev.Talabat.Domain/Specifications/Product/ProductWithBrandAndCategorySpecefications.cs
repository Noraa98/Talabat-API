using LinkDev.Talabat.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Domain.Specifications.Products
{
    public class ProductWithBrandAndCategorySpecefications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndCategorySpecefications()
            : base()
        {
            AddIncludes();
        }

        public ProductWithBrandAndCategorySpecefications(int id)
            : base(id)
        {
            AddIncludes();
        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(p => p.Brand!);
            Includes.Add(p => p.Category!);
        }



    }
}
