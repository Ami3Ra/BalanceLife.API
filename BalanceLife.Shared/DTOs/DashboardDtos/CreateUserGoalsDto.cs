using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanceLife.Shared.DTOs.DashboardDtos
{
    public class CreateUserGoalsDto
    {
        public int CaloriesGoal { get; set; }
        public int WaterGoal { get; set; }
        public int ActivityGoal { get; set; }
    }
}
