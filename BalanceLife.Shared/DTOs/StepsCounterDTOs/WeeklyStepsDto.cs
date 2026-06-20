using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.StepsCounterDTOs
{
    public class WeeklyStepsDto
    {
        public string Day { get; set; } = default!;
        public int Steps { get; set; }
    }
}
