using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.SportDTOs
{
    public class WorkoutHistoryDto
    {
        public string WorkoutName { get; set; } = default!;
        public DateTime Date { get; set; }
        public double Duration { get; set; }
        public int CaloriesBurned { get; set; }
    }
}
