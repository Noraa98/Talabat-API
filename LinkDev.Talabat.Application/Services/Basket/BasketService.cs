using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Application.Exceptions;
using LinkDev.Talabat.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Application.Services.Basket
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public BasketService(IBasketRepository basketRepository , IMapper mapper , IConfiguration configuration)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
         
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await _basketRepository.GetAsync(id);

            if (basket == null)
                throw new NotFoundException(nameof(Domain.Entities.Basket.Basket) , id);

            var basketDto = _mapper.Map<BasketDto>(basket);
            return basketDto;

        }
        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var mappedBasket = _mapper.Map<Domain.Entities.Basket.Basket>(basket);

            var daysToLive =int.Parse( _configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!);


            var updatedBasket = await _basketRepository.UpdateAsync(mappedBasket, TimeSpan.FromDays(daysToLive));
            if (updatedBasket is  null)
                throw new BadRequestException("Failed to update the basket.");
            
            return basket;
        }
        public async Task DeleteBasketAsync(string basketId)
        {
            await _basketRepository.DeleteAsync(basketId);
        }





        }
}
