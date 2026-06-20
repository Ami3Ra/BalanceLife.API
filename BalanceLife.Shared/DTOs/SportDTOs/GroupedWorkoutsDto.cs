using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.SportDTOs
{
    public class GroupedWorkoutsDto
    {
        public List<WorkoutDTO> Cardio { get; set; } = new();
        public List<WorkoutDTO> Strength { get; set; } = new();
        public List<WorkoutDTO> Flexibility { get; set; } = new();
        public List<WorkoutDTO> Home { get; set; } = new();
    }
}
