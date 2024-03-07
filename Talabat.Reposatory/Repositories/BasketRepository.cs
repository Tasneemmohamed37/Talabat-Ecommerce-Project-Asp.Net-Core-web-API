using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Entities.Cart;
using Talabat.Core.Interfaces;

namespace Talabat.Reposatory.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)  // Ask CLR to inject obj from class 'redisDB'
        { 
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId); 
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
             var basket = await _database.StringGetAsync(basketId);
            return basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var updatedOrCreatedBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(1));
            if (updatedOrCreatedBasket is false)
                return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
