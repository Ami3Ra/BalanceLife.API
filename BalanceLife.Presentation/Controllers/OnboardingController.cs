using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Services.Abstraction;
using BalanceLife.Shared.DTOs.OnboardingDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BalanceLife.Presentation.Controllers
{
    public class OnboardingController : ApiBaseController
    {
        private readonly IOnboardingService _onboardingService;

        public OnboardingController(
            IOnboardingService onboardingService)
        {
            _onboardingService = onboardingService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveProfile(
            CreateUserProfileDto dto)
        {
            var userId =
                User.FindFirst(
                    System.Security.Claims.ClaimTypes.NameIdentifier)
                ?.Value;

            await _onboardingService
                .SaveProfileAsync(userId!, dto);

            return Ok(new
            {
                Message = "Profile saved successfully"
            });
        }


        [HttpGet("result")]
        [Authorize]
        public async Task<IActionResult> GetResult()
        {
            var userId =
                User.FindFirst(
                    ClaimTypes.NameIdentifier)?.Value;

            var userName =
                User.FindFirst(
                    ClaimTypes.Name)?.Value;

            var result =
                await _onboardingService
                    .GetResultAsync(
                        userId!,
                        userName!);

            return Ok(result);
        }
    }
}
