using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.SportDTOs
{
    public class WorkoutSessionDto
    {
        public int SessionId { get; set; }
        public string WorkoutName { get; set; } = default!;
        public DateTime StartTime { get; set; }
    }
}
