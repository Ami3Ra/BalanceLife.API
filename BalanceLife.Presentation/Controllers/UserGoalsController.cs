using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.DashboardDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class UserGoalsController : ApiBaseController
    {
        private readonly IUserGoalsService _service;

        public UserGoalsController(IUserGoalsService service)
        {
            _service = service;
        }

        [HttpPost("set-default")]
        [Authorize]
        public async Task<IActionResult> SetDefault()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await _service.SetDefaultGoalsAsync(userId);

            return Ok("Goals initialized");
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(CreateUserGoalsDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await _service.UpdateGoalsAsync(userId, dto);

            return Ok("Goals updated");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await _service.GetGoalsAsync(userId);

            return Ok(result);
        }

    }
}
