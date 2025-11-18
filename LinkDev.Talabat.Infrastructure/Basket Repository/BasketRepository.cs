using LinkDev.Talabat.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace LinkDev.Talabat.Infrastructure.Basket_Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<Basket?> GetAsync(string id)
        {
            var data = await _database.StringGetAsync(id);

            if (data.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<Basket>(data!);
        }

        public async Task<Basket?> UpdateAsync(Basket basket, TimeSpan timeToLive)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);
            var updated = await _database.StringSetAsync(basket.Id, serializedBasket, timeToLive);

            if (!updated) return null;
            return await GetAsync(basket.Id);
        }

        public async Task DeleteAsync(string id)
        {
            await _database.KeyDeleteAsync(id);
        }
    }
}
