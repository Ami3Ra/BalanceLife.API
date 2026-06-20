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
    public class BreathingExercisesController : ApiBaseController
    {
        private readonly IBreathingExerciseService _breathing;

        public BreathingExercisesController(IBreathingExerciseService breathing)
        {
            _breathing = breathing;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var data = await _breathing.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _breathing.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }
    }
}
