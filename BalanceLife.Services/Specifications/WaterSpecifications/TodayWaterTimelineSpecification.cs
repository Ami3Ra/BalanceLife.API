using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.WaterModule;

namespace BalanceLife.Services.Specifications.WaterSpecifications
{
    public class TodayWaterTimelineSpecification : BaseSpecifications<WaterIntake, int>
    {
        public TodayWaterTimelineSpecification(string userId, DateTime date)
        : base(w => w.UserId == userId &&
                    w.CreatedAt.Date == date)
        {
            AddOrderByDescending(w => w.CreatedAt); // أحدث الأول
        }
    }
}
