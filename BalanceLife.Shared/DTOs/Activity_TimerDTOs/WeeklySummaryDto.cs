using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.Activity_TimerDTOs
{
    public class WeeklySummaryDto
    {
        public int TotalWorkouts { get; set; }
        public int TotalDuration { get; set; }
        public int TotalCalories { get; set; }
    }
}
