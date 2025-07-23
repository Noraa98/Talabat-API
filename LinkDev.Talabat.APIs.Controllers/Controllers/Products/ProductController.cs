using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet] // GET: api/products
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var products = await serviceManager.ProductService.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id:int}")] // GET: api/products/{id}
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProductdAsync(id);
            if (product == null)
            {
                return NotFound(new { statusCode = 404, messege = "Not Found" });
            }
            return Ok(product);
        }

        [HttpGet("brands")] // GET: api/products/brands
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("categories")] // GET: api/products/categories
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await serviceManager.ProductService.GetCategoriesAsync();
            return Ok(categories);

        }
    }
}
