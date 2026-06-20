using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.SportModule
{
    public class StepRecord : BaseEntity<int>
    {
        public int Id { get; set; }

        public string UserId { get; set; } = default!;

        public DateTime Date { get; set; }

        public int Steps { get; set; }

        public decimal Distance { get; set; }

        public int Calories { get; set; }

        public int ActiveTime { get; set; }
    }
}
