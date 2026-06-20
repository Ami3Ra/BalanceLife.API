using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.WaterDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class WaterController : ApiBaseController
    {
        private readonly IWaterService _waterService;

        public WaterController(IWaterService waterService)
        {
            _waterService = waterService;
        }

        // Add Water
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddWater(AddWaterDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _waterService.AddWaterAsync(userId, dto.AmountInMl);

            return Ok("Water added successfully 💧");
        }

        [HttpGet("today")]
        [Authorize]
        public async Task<ActionResult<WaterTodayDto>> GetToday()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _waterService.GetTodayWaterAsync(userId);

            return Ok(result);
        }

        [HttpGet("streak")]
        [Authorize]
        public async Task<ActionResult<WaterStreakDto>> GetStreak()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _waterService.GetStreakAsync(userId);

            return Ok(result);
        }

        [HttpGet("weekly")]
        [Authorize]
        public async Task<ActionResult<WeeklyWaterDto>> GetWeekly()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _waterService.GetWeeklyWaterAsync(userId);

            return Ok(result);
        }

        [HttpGet("timeline")]
        [Authorize]
        public async Task<ActionResult<List<WaterTimelineDto>>> GetTimeline()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _waterService.GetTodayTimelineAsync(userId);

            return Ok(result);
        }
    }
}
