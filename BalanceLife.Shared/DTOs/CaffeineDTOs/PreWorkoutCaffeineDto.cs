using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.CaffeineDTOs
{
    public class PreWorkoutCaffeineDto
    {
        public string Title { get; set; } = default!;
        public string Time { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Type { get; set; } = default!; // Good / Caution
    }
}
