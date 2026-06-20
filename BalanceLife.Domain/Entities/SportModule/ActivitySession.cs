using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.SportModule
{
    public class ActivitySession : BaseEntity<int>
    {
        public string UserId { get; set; } = default!;

        public string ActivityType { get; set; } = default!; // Cardio, Yoga...

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double CaloriesBurned { get; set; }
    }
}
