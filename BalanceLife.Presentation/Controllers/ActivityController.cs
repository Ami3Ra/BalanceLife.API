using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Contracts;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.Activity_TimerDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class ActivityController : ApiBaseController
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost("start")]
        [Authorize]
        public async Task<IActionResult> Start(StartActivityDto dto)
        {
            var userId = "test-user";

            var id = await _activityService.StartAsync(userId, dto.ActivityType);

            return Ok(id);
        }

        [HttpPost("end")]
        [Authorize]
        public async Task<IActionResult> End(EndActivityDto dto)
        {
            var userId = "test-user";

            var result = await _activityService.EndAsync(userId, dto.SessionId);

            return Ok(result);
        }

        [HttpGet("active")]
        [Authorize]
        public async Task<IActionResult> Active()
        {
            var userId = "test-user";

            var session = await _activityService.GetActiveAsync(userId);

            if (session == null)
                return NoContent();

            return Ok(session);
        }


        [HttpGet("history")]
        [Authorize]
        public async Task<IActionResult> History()
        {
            var userId = "test-user";

            var result = await _activityService.GetHistoryAsync(userId);

            return Ok(result);
        }

        [HttpGet("weekly-summary")]
        [Authorize]
        public async Task<IActionResult> WeeklySummary()
        {
            var userId = "test-user";

            var result = await _activityService.GetWeeklySummaryAsync(userId);

            return Ok(result);
        }

    }
}
