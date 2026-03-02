using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Services.Abstraction;

namespace BalanceLife.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }
        public async Task<string?> GetAsync(string cachekey)
        {
            return await _cacheRepository.GetAsync(cachekey);
        }

        public async Task SetAsync(string cachekey, object cacheValue, TimeSpan TimeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue,
               new JsonSerializerOptions(){
                   PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _cacheRepository.SetAsync(cachekey, value, TimeToLive);
        }
    }
}
