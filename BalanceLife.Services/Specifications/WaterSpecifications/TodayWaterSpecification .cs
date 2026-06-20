using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.WaterModule;

namespace BalanceLife.Services.Specifications.WaterSpecifications
{
    internal class TodayWaterSpecification : BaseSpecifications<WaterIntake, int>
    {
        public TodayWaterSpecification(string userId, DateTime date)
          : base(w =>
              w.UserId == userId &&
              w.CreatedAt.Date == date)
        {
        }
    }
}
