using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.WaterModule;

namespace BalanceLife.Services.Specifications.WaterSpecifications
{
    internal class WaterAllSpecification : BaseSpecifications<WaterIntake, int>
    {
        public WaterAllSpecification(string userId)
        : base(w => w.UserId == userId)
        {
        }
    }
}
