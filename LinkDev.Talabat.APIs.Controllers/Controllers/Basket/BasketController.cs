using LinkDev.Talabat.APIs.Controllers.Base;
using LinkDev.Talabat.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Application.Abstraction.Services;
using LinkDev.Talabat.Application.Abstraction.Services.Basket;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Basket
{
    public class BasketController : BaseApiController
    {
        private readonly IServiceManager _serviceManager;

        public BasketController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/basket?id=customer123
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var response = await _serviceManager.BasketService.GetBasketAsync(id);

            return Ok(response);
        }

      
        
        [HttpPost] // POST: api/basket
        public async Task<ActionResult<BasketDto>> UpdateBasket(BasketDto basket)
        {
            var updatedBasket = await _serviceManager.BasketService.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        
        [HttpDelete]  // DELETE: api/basket?id=customer123
        public async Task<IActionResult> DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(id);
            return Ok();
        }
    }


}

