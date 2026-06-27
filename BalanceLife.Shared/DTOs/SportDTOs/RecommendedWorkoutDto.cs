using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.SportDTOs
{
    public class RecommendedWorkoutDto
    {
        public string CurrentGoal { get; set; } = default!;

        public string WorkoutType { get; set; } = default!;

        public int Duration { get; set; }

        public int ExpectedBurn { get; set; }

        public string Tip { get; set; } = default!;
    }
}
