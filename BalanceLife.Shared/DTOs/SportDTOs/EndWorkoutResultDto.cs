using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.SportDTOs
{
    public class EndWorkoutResultDto
    {
            public double Duration { get; set; }
            public int CaloriesBurned { get; set; }
            public bool AlreadyEnded { get; set; }

    }
}
