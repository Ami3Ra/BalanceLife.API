using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.BreathingExerciseDTOs
{
    public class BreathingExerciseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int Duration { get; set; }
        public string Pattern { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Benefit { get; set; } = default!;
    }
}
