using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.Activity_TimerDTOs
{
    public class ActivityHistoryDto
    {
        public string ActivityType { get; set; } = default!;
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
    }
}
