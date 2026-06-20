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
    public class WorkoutVideosController : ApiBaseController
    {
        private readonly IWorkoutVideoService _service;

        public WorkoutVideosController(IWorkoutVideoService service)
        {
            _service = service;
        }

        // 🔥 Get All OR Filtered
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? level,
            [FromQuery] int? maxDuration)
        {
            if (!string.IsNullOrEmpty(level) || maxDuration.HasValue)
            {
                var filtered = await _service.GetFilteredAsync(level, maxDuration);
                return Ok(filtered);
            }

            var result = await _service.GetAllAsync();
            return Ok(result);
        }


        // 🔥 Get By Id
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var video = await _service.GetByIdAsync(id);

            if (video == null)
                return NotFound(new { message = "Video not found" });

            return Ok(video);
        }
    }
}
