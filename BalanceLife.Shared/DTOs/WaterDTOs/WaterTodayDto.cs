using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.WaterDTOs
{
    public class WaterTodayDto
    {
        public int TotalMl { get; set; }
        public int GoalMl { get; set; } = 2000;
        public double Percentage { get; set; }
        public int Cups { get; set; }
        public bool IsGoalCompleted { get; set; }
        public string StatusMessage { get; set; } = default!;
    }
}
