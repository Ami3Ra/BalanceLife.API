using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.AICoachDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class AICoachController : ApiBaseController
    {
        private readonly IAICoachService _aiCoachService;

        public AICoachController(
            IAICoachService aiCoachService)
        {
            _aiCoachService = aiCoachService;
        }


        [HttpPost("chat")]
        [Authorize]
        public async Task<IActionResult> Chat(
        ChatRequestDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var reply = await _aiCoachService.AskAsync(
                               userId,
                                 userName,
                     dto.Message);

            return Ok(new
            {
                reply
            });


        }
    }
}