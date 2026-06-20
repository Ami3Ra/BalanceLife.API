using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.WaterModule;

namespace BalanceLife.Services.Specifications.WaterSpecifications
{
    public class WeeklyWaterSpecification : BaseSpecifications<WaterIntake, int>
    {
        public WeeklyWaterSpecification(string userId, DateTime startDate, DateTime endDate)
        : base(w => w.UserId == userId &&
                    w.CreatedAt >= startDate &&
                    w.CreatedAt < endDate)
        {
        }
    }
}
