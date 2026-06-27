using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Domain.Entities.DashboardModule
{
    public class UserGoals : BaseEntity<int>
    {
        public string UserId { get; set; } = default!;

        public int CaloriesGoal { get; set; }
        public int WaterGoal { get; set; }
        public int ActivityGoal { get; set; }
    }
}
