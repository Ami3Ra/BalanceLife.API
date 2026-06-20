using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.WaterDTOs
{
    public class WaterStreakDto
    {
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public bool IsTodayCompleted { get; set; }
    }
}
