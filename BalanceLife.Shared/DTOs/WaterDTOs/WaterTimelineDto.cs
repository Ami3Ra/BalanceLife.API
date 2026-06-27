using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.WaterDTOs
{
    public class WaterTimelineDto
    {
        public int Id { get; set; }

        public string Time { get; set; } = default!;

        public int AmountInMl { get; set; }
    }
}
