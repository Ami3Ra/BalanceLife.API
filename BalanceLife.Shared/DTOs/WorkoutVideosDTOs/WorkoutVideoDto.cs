using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.WorkoutVideosDTOs
{
    public class WorkoutVideoDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Trainer { get; set; } = default!;
        public int Duration { get; set; }
        public int Calories { get; set; }
        public string Level { get; set; } = default!;
        public string Icon { get; set; } = default!;
        public string VideoUrl { get; set; } = default!;
    }
}
