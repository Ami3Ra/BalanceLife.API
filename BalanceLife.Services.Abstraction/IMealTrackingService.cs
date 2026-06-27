using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Services.Abstraction
{
    public interface IMealTrackingService
    {
        Task AddMealLogAsync(string userId, int mealId);
        Task<int> GetTodayCaloriesAsync(string userId);
    }
}
