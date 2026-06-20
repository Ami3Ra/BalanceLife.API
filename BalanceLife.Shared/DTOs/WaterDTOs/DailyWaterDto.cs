using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.WaterDTOs
{
    public class DailyWaterDto
    {
            public string DayName { get; set; } = default!;
            public int TotalMl { get; set; }
            public int DisplayMl { get; set; }
            public bool IsOverGoal { get; set; }
        
    }
}
