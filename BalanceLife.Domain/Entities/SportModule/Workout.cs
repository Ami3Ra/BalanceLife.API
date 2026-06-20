using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.SportModule.Enums;

namespace BalanceLife.Domain.Entities.SportModule
{
    public class Workout : BaseEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Category { get; set; } = default!;  // Cardio, Strength...

        public WorkoutLevel Level { get; set; }     // Beginner, Intermediate, Advanced

        public int DurationInMinutes { get; set; }

        public int Calories { get; set; }
    }
}
