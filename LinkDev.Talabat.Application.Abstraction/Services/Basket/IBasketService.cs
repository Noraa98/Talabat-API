using LinkDev.Talabat.Application.Abstraction.Models.Basket;

namespace LinkDev.Talabat.Application.Abstraction.Services.Basket
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string id);
        Task<BasketDto?> UpdateBasketAsync(BasketDto basket);
        Task DeleteBasketAsync(string basketId);
    }
}
