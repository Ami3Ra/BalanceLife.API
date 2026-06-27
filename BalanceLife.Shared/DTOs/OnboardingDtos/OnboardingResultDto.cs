using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.OnboardingDtos
{
    public class OnboardingResultDto
    {
        public string Name { get; set; } = default!;
        public string Goal { get; set; } = default!;

        public int CaloriesGoal { get; set; }
        public int WaterGoal { get; set; }
        public int ActivityGoal { get; set; }

    }
}
