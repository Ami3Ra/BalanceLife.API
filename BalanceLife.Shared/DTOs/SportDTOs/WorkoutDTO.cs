using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.SportDTOs
{
    public class WorkoutDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Level { get; set; } = default!;
        public int Duration { get; set; }
        public int Calories { get; set; }
    }
}
