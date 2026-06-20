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
    public class CaffeineController : ApiBaseController
    {
        private readonly ICaffeineService _service;

        public CaffeineController(ICaffeineService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTips()
        {
            var result = await _service.GetTipsAsync();
            return Ok(result);
        }


        [HttpGet("guide")]
        [Authorize]
        public async Task<IActionResult> GetGuide()
        {
            var result = await _service.GetGuideAsync();
            return Ok(result);
        }


        [HttpGet("pre-workout")]
        [Authorize]
        public async Task<IActionResult> GetPreWorkout()
        {
            var result = await _service.GetPreWorkoutAsync();
            return Ok(result);
        }

        [HttpGet("reference")]
        [Authorize]
        public async Task<IActionResult> GetReference()
        {
            var result = await _service.GetReferenceAsync();
            return Ok(result);
        }
    }
}
