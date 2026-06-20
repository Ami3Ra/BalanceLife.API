using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.SportDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class WorkoutsController : ApiBaseController
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAll([FromQuery] string? category, [FromQuery] string? level)
        {
            var result = await _workoutService.GetAllAsync(category, level);
            return Ok(result);
        }


        [HttpGet("grouped")]
        [Authorize]
        public async Task<ActionResult> GetGrouped()
        {
            var result = await _workoutService.GetGroupedAsync();
            return Ok(result);
        }


        [HttpPost("start")]
        [Authorize]
        public async Task<ActionResult> StartWorkout([FromBody] StartWorkoutDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _workoutService.StartWorkoutAsync(userId, dto.WorkoutId);

            return Ok(result);
        }

        [HttpPost("end")]
        [Authorize]
        public async Task<ActionResult> EndWorkout([FromBody] EndWorkoutDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _workoutService.EndWorkoutAsync(userId, dto.SessionId);

            return Ok(result);
        }



        [HttpGet("active")]
        [Authorize]
        public async Task<IActionResult> Active()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _workoutService.GetActiveSessionAsync(userId);

            return Ok(result);
        }



        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _workoutService.GetHistoryAsync(userId);

            return Ok(result);
        }



        [HttpGet("today-summary")]
        [Authorize]
        public async Task<IActionResult> TodaySummary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _workoutService.GetTodaySummaryAsync(userId);

            return Ok(result);
        }
    }
}
