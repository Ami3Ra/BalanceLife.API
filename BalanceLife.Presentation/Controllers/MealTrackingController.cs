using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.MealDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class MealTrackingController : ApiBaseController
    {
        private readonly IMealTrackingService _mealTrackingService;

        public MealTrackingController(IMealTrackingService mealTrackingService)
        {
            _mealTrackingService = mealTrackingService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMeal([FromBody] AddMealLogDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await _mealTrackingService.AddMealLogAsync(userId, dto.MealId);

            return Ok("Meal Added");
        }
    }
}
