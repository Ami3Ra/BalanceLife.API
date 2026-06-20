using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.SportModule
{
    public class WorkoutSession : BaseEntity<int>
    {
        public string UserId { get; set; } = default!;

        public int WorkoutId { get; set; }
        public Workout Workout { get; set; } = default!;

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int CaloriesBurned { get; set; }
    }
}
