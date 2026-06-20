using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.WaterModule
{
    public class WaterIntake:BaseEntity<int>
    {
        public string UserId { get; set; } = default!;

        public int AmountInMl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
