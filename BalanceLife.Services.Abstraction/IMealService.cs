using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Shared;
using BalanceLife.Shared.CommonResponses;
using BalanceLife.Shared.DTOs.MealDTOs;

namespace BalanceLife.Services.Abstraction
{
    public interface IMealService
    {
        Task<PaginatedResult<MealDTO>> GetAllMealsAsync(MealQueryParams queryParams);
        Task<Result<MealDTO>> GetMealByIdAsync(int id);
        Task<IEnumerable<RestaurantDTO>> GetAllRestaurantsAsync(double userLat, double userLng);
    }
}
