using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.StepsCounterDTOs
{
    public class TodayStepsDto
    {
        public int Steps { get; set; }
        public int Goal { get; set; } = 10000;
        public int Remaining { get; set; }

        public double Distance { get; set; }
        public int Calories { get; set; }
        public int ActiveTime { get; set; }
    }
}
