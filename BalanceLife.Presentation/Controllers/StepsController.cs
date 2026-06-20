using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class StepsController : ApiBaseController
    {
        private readonly IStepsService _stepsService;

        public StepsController(IStepsService stepsService)
        {
            _stepsService = stepsService;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddSteps([FromBody] int steps)
        {
            var userId = "test-user";

            await _stepsService.AddStepsAsync(userId, steps);

            return Ok(new { message = "Steps added successfully" });
        }


        [HttpGet("today")]
        [Authorize]
        public async Task<IActionResult> GetToday()
        {
            var userId = "test-user";

            var result = await _stepsService.GetTodayAsync(userId);

            return Ok(result);
        }


        [HttpPost("reset")]
        [Authorize]
        public async Task<IActionResult> Reset()
        {
            var userId = "test-user";

            await _stepsService.ResetTodayAsync(userId);

            return Ok(new { message = "Steps reset successfully" });
        }


        [HttpGet("weekly")]
        [Authorize]
        public async Task<IActionResult> GetWeekly()
        {
            var userId = "test-user";

            var result = await _stepsService.GetWeeklyAsync(userId);

            return Ok(result);
        }
    }
}
