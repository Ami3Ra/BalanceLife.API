using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.WaterDTOs
{
    public class WeeklyWaterDto
    {
        public List<DailyWaterDto> Days { get; set; } = new();
        public int WeeklyTotal { get; set; }
        public double DailyAverage { get; set; }
    }
}
