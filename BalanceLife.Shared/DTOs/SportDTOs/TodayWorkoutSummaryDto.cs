using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.SportDTOs
{
    public class TodayWorkoutSummaryDto
    {
        public int TotalWorkouts { get; set; }
        public double TotalDuration { get; set; }
        public int TotalCalories { get; set; }
    }
}
