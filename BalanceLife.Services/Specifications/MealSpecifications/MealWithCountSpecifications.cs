using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BalanceLife.Domain.Entities.MealModule;
using BalanceLife.Shared;

namespace BalanceLife.Services.Specifications.MealSpecifications
{
    internal class MealWithCountSpecifications:BaseSpecifications<Meal, int>
    {
        public MealWithCountSpecifications(MealQueryParams queryParams) 
            : base(MealSpecificationHelper.BuildCriteria(queryParams))
        {
            
        }

    }
}
