using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.OnboardingModule
{
    public class UserProfile : BaseEntity<int>
    {
        public string UserId { get; set; } = default!;
        public int Age { get; set; }

        public string Gender { get; set; } = default!;

        public double Height { get; set; }

        public double Weight { get; set; }

        public string Goal { get; set; } = default!;

        public string ActivityLevel { get; set; } = default!;

        public int SleepHours { get; set; }

        public int WaterIntake { get; set; }

        public string JobType { get; set; } = default!;
    }
}
