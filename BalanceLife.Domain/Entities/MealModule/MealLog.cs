using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.MealModule
{
    public class MealLog:BaseEntity<int>
    {

        public string UserId { get; set; } = default!;

        public int MealId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
