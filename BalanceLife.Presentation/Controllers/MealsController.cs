using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Presentation.Attributes;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared;
using BalanceLife.Shared.DTOs.MealDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{

    public class MealsController : ApiBaseController
    {
        private readonly IMealService _mealService;

        public MealsController(IMealService mealService)
        {
            _mealService = mealService;
        }

        // Get All Meals
        [HttpGet]
        // GET:baseUrl/api/Meals
        [Authorize]
        //[RedisCache]
        public async Task<ActionResult<PaginatedResult<MealDTO>>> GetAllMeals(
           [FromQuery] MealQueryParams queryParams)
        {
            var meals = await _mealService.GetAllMealsAsync(queryParams);
            return Ok(meals);
        }

        [HttpGet("{id}")]
        // GET:baseUrl/api/Meals/1
        public async Task<ActionResult<MealDTO>> GetMeal(int id)
        {
            var meal = await _mealService.GetMealByIdAsync(id);
            return HandleResult<MealDTO>(meal);
        }

        [HttpGet("nearby")]
        // GET:baseUrl/api/Restaurants
        public async Task<ActionResult<RestaurantDTO>> GetAllRestaurants([FromQuery] double lat, [FromQuery] double lng)
        {
            var restaurants = await _mealService.GetAllRestaurantsAsync(lat, lng);
            return Ok(restaurants);
        }
    }
}
