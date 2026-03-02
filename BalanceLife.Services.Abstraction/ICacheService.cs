using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Services.Abstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string cachekey);

        Task SetAsync(string cachekey, object cacheValue, TimeSpan TimeToLive);
    }
}
