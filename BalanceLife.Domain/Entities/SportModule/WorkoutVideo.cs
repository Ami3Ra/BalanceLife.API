using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.SportModule
{
    public class WorkoutVideo : BaseEntity<int>
    {
        public string Title { get; set; } = default!;
        public string Trainer { get; set; } = default!;
        public int Duration { get; set; }
        public int Calories { get; set; }
        public string Level { get; set; } = default!;
        public string Icon { get; set; } = default!;
        public string VideoUrl { get; set; } = default!;
    }
}
